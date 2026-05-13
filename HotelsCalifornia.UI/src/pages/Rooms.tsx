import RoomCard from "../components/RoomCard"
import type { Room } from "../types/Room"
import { useState, useEffect } from "react"
import { roomService } from "../services/roomService"

export default function Rooms()
{
  const [roomList, setRoomList] = useState<Room[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() =>
  {
    roomService.getAllRooms()
      .then((data) =>
      {
        console.log("API data =", data);
        console.log("Type =", Array.isArray(data));
        setRoomList(data)
      })
      .catch((err) => setError(err.message ?? 'Failed to load rooms'))
      .finally(() => setIsLoading(false))
  }, [])

  if (isLoading) return <main><p>Loading Rooms...</p></main>
  if (error) return <main><p style={{ color: 'red' }}>{error}</p></main>

  return (
    <main>
      <h2>Rooms for All the Hotels!</h2>
      <div>
        {
          roomList.length === 0 ?
            (
              <p>Sorry! There's no rooms</p>
            ) :

            roomList.map((r) => <RoomCard key={r.id} room={r} />)

        }
      </div>
    </main>
  )
}
