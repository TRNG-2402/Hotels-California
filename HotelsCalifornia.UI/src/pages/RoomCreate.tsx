import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { roomService } from "../services/roomService";

export default function CreateRoom()
{
    const { hotelId } = useParams();
    const navigate = useNavigate();

    const [room, setRoom] = useState({
        hotelId: Number(hotelId),
        roomNumber: 0,
        dailyRate: 0,
        numBeds: 1,
        description: ""
    });

    const handleSubmit = async (e: React.SubmitEvent) =>
    {
        try
        {
            e.preventDefault();
            await roomService.createRoom(room);
            navigate(`/hotels/${hotelId}/rooms`);
            alert("Room created!");
        }
        catch (err: any)
        {
            throw new Error(err.response?.data?.message ?? "Failed to create a room...")
        }
    };

    return (
        <main>
            <h2>Create Room for Hotel {hotelId}</h2>

            <form onSubmit={handleSubmit}>
                <label> Room Number
                    <input
                        type="text"
                        placeholder="Room Number"
                        value={room.roomNumber}
                        onChange={(e) => setRoom({ ...room, roomNumber: Number(e.target.value) })}
                    />
                </label>

                <br/>

                <label> Daily Rate
                    <input
                        type="number"
                        placeholder="Daily Rate"
                        value={room.dailyRate}
                        onChange={(e) => setRoom({ ...room, dailyRate: Number(e.target.value) })}
                    />
                </label>

                <br/>

                <label> Number of Beds
                    <input
                        type="number"
                        placeholder="Beds"
                        value={room.numBeds}
                        onChange={(e) => setRoom({ ...room, numBeds: Number(e.target.value) })}
                    />
                </label>
                <br/>

                <label>Descriptions (Optional)
                    <textarea
                        placeholder="Description"
                        value={room.description}
                        onChange={(e) => setRoom({ ...room, description: e.target.value })}
                    />
                </label>
                <br/>
                <button type="submit">Create</button>
            </form>
        </main>
    );
}
