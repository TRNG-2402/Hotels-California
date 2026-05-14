import RoomCard from "../components/RoomCard"
import type { Room } from "../types/Room"
import { useState, useEffect } from "react"
import { roomService } from "../services/roomService"
import { Link, useParams } from "react-router-dom";

export default function RoomsOfHotel()
{
    const { hotelId } = useParams();
    const [roomList, setRoomList] = useState<Room[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() =>
    {
        roomService.getRoomsByHotelId(Number(hotelId))
            .then((data) =>
            {
                console.log("API data =", data);
                console.log("Type =", Array.isArray(data));
                setRoomList(data)
            })
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
                        roomList.map((r) => <RoomCard key={r.id} room={r} />)
                }
            </div>
            <Link to={`/hotels/${hotelId}/rooms/create`}>
                <button>Click me to create a new room for hotel {hotelId}</button>
            </Link>
        </main>
    )
}
