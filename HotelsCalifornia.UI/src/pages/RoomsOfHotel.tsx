import { RoomCardWithDelete } from "../components/RoomCard"
import type { Room } from "../types/Room"
import { useState, useEffect } from "react"
import { roomService } from "../services/roomService"
import { Link, useParams } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

export default function RoomsOfHotel()
{
    const { hotelId } = useParams();
    const [roomList, setRoomList] = useState<Room[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const { user } = useAuth();

    const handleDelete = async (id: number) =>
    {
        if (!confirm("Will delete the room")) return;

        try
        {
            await roomService.deleteRoom(id);
            alert("Room deleted!")
            setRoomList(prev => prev.filter(r => r.id !== id));
        }
        catch (err: any)
        {
            throw new Error(err.response?.data?.message ?? "Failed to delete the room...")
        }
    }

    useEffect(() =>
    {
        roomService.getRoomsByHotelId(Number(hotelId))
            .then((data) => setRoomList(data))
            .catch((err) => setError(err.message ?? 'Failed to load rooms'))
            .finally(() => setIsLoading(false))
    }, [hotelId])

    if (isLoading) return <main><p>Loading Rooms...</p></main>
    if (error) return <main><p style={{ color: 'red' }}>{error}</p></main>

    return (
        <main>
            <h2>Rooms for Hotel {hotelId}</h2>
            <div>
                {
                    roomList.length === 0 ? (
                        <p>Sorry! There's no room for id={hotelId}</p>
                    ) :
                        roomList.map((r) =>
                            <RoomCardWithDelete key={r.id} room={r} onDelete={handleDelete} />
                        )
                }
            </div>
            <Link to={`/hotels/${hotelId}/rooms/create`}>
                {user?.role === "Manager" && user?.hotelId === Number(hotelId) && <button>Click me to create a new room for hotel {hotelId}</button>}
            </Link>
        </main>
    )
}
