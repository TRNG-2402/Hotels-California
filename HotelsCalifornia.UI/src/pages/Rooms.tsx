import RoomCard from "../components/RoomCard"
import type { Room } from "../types/Room"
import { useState, useEffect } from "react"
import { roomService } from "../services/roomService"
import { useParams } from "react-router-dom";
// import { useParams } from "react-router-dom";


export default function Rooms()
{
  const { hotelId } = useParams();
  const [roomList, setRoomList] = useState<Room[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  // assume this is the rooms of specific hotels from /api/Room/room/hotel/{hotelId}
  const testRooms: Room[] = [
    {
      id: 1,
      hotelId: 1,
      roomNumber: 101,
      dailyRate: 5,
      numBeds: 3,
      description: "something"
    },
    {
      id: 2,
      hotelId: 1,
      roomNumber: 102,
      dailyRate: 4.5,
      numBeds: 4,
      description: "something"
    }
  ]
  // useEffect(() =>
  // {
  //   roomService.getRoomsByHotelId(Number(hotelId))
  //     .then((data) =>
  //     {
  //       console.log("API data =", data);
  //       console.log("Type =", Array.isArray(data));
  //       setRoomList(data)
  //     })
  //     .catch((err) => setError(err.message ?? 'Failed to load rooms'))
  //     .finally(() => setIsLoading(false))
  // }, [hotelId])
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

  // if (isLoading) return <main><p>Loading Rooms...</p></main>
  // if (error) return <main><p style={{ color: 'red' }}>{error}</p></main>

  return (
    <main>
      <h2>Rooms for Hotels</h2>
      <div>
        {
          roomList.length === 0 ?
            (
              <p>Sorry! There's no room for {hotelId}</p>
            ) :

            roomList.map((r) => <RoomCard key={r.id} room={r} />)

        }
      </div>
    </main>
  )
}
