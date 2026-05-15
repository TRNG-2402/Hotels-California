import type { Room } from "../types/Room"
//TODO make own styles for room
import styles from '../styles/RoomCard.module.css'
import { Link } from "react-router-dom"
import { useAuth } from "../context/AuthContext";

interface RoomCardProps
{
    room: Room
}

interface RoomCardWithDeleteProps extends RoomCardProps
{
    onDelete: (id: number) => void;
}

export function RoomCard({ room }: RoomCardProps)
{
    const { user } = useAuth();

    return (
        <div className={styles.card}>
            <h3>{room.roomNumber}</h3>
            <p>{room.description}</p>
            <p>Number of beds: {room.numBeds}</p>
            <p>Daily Rate: {room.dailyRate}</p>

            {user?.role === "Manager" && user?.hotelId === room.hotelId && <Link to={`/rooms/edit/${room.id}`}>Edit</Link>}
        </div>
    )
}

export function RoomCardWithDelete({ room, onDelete }: RoomCardWithDeleteProps)
{
    const { user } = useAuth();
    return (
        <div className={styles.card}>
            <h3>{room.roomNumber}</h3>
            <p>(Test only)ID: {room.id}</p>
            <p>{room.description}</p>
            <p>Number of beds: {room.numBeds}</p>
            <p>Daily Rate: {room.dailyRate}</p>

            <Link to={`/reservation/create/${room.id}`}>Reserve Now!</Link>
            <> </>
            {user?.role === "Manager" && user?.hotelId === room.hotelId && <Link to={`/rooms/edit/${room.id}`}>Edit</Link>}
            <> </>
        </div>
    )
}