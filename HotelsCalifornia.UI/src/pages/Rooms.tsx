import RoomCard from "../components/RoomCard"
import type { Room } from "../types/Room"
import { useState, useEffect } from "react"
import { roomService } from "../services/roomService"
import { useParams } from "react-router-dom";


export default function Rooms()
{
  const { hotelId } = useParams();
  const [roomList, setRoomList] = useState<Room[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() =>
  {
    roomService.getRoomsByHotelId(hotelId!)
      .then((data) => setRoomList(data))
      .catch((err) => setError(err.message ?? 'Failed to load rooms'))
      .finally(() => setIsLoading(false))
  }, [hotelId])

  if (isLoading) return <main><p>Loading Rooms...</p></main>
  if (error) return <main><p style={{ color: 'red' }}>{error}</p></main>

  return (
    <main>
      <h2>Rooms for Hotels</h2>
      {
        // roomList.map((r) => (
        //   <RoomCard key={r.roomId} room={r} />
        // ))
        roomList.length === 0 ?
          (
            <p>Sorry! There's no room for {hotelId}</p>
          ) :
          (
            roomList.map((r) => <RoomCard key={r.roomId} room={r} />)
          )
      }

    </main>
  )
}
