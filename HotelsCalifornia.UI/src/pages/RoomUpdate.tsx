import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { roomService } from "../services/roomService";
import type { Room } from "../types/Room";


export default function RoomUpdate()
{
    const { roomId } = useParams();
    const [room, setRoom] = useState<Room | null>(null);
    const navigate = useNavigate();

    useEffect(() =>
    {
        roomService.getRoomById(Number(roomId))
            .then(setRoom)
            .catch(console.error);
    }, [roomId]);

    const handleSubmit = async (e: React.SubmitEvent) =>
    {
        try
        {
            e.preventDefault();
            if (!room) return;
            await roomService.updateRoom(room);
            alert("Room updated!");
            navigate(-1);
        }

        catch (err: any)
        {
            throw new Error(err.response?.data?.message ?? "Failed to update the room...")
        }

    };

    if (!room) return <p>Loading...</p>;

    return (
        <form onSubmit={handleSubmit}>
            <label>
                Room Number:
                <input
                    value={room.roomNumber}
                    onChange={e => setRoom({ ...room, roomNumber: Number(e.target.value) })}
                />
            </label>

            <label>
                Daily Rate:
                <input
                    type="number"
                    value={room.dailyRate}
                    onChange={e => setRoom({ ...room, dailyRate: Number(e.target.value) })}
                />
            </label>

            <label>
                Num Beds:
                <input
                    type="number"
                    value={room.numBeds}
                    onChange={e => setRoom({ ...room, numBeds: Number(e.target.value) })}
                />
            </label>

            <label>
                Description:
                <input
                    value={room.description}
                    onChange={e => setRoom({ ...room, description: e.target.value })}
                />
            </label>

            <button type="submit">Save</button>
        </form>
    );
}
