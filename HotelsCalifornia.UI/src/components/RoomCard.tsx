import type { Room } from "../types/Room"
//TODO make own styles for room
import styles from '../styles/HotelCard.module.css'
import { Link } from "react-router-dom"

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
    return (
        <div className={styles.card}>
            <h3>{room.roomNumber}</h3>
            <p>(Test only)ID: {room.id}</p>
            <p>{room.description}</p>
            <p>Number of beds: {room.numBeds}</p>
            <p>Daily Rate: {room.dailyRate}</p>

            <Link to={`/rooms/edit/${room.id}`}>Edit</Link>
        </div>
    )
}

export function RoomCardWithDelete({ room, onDelete }: RoomCardWithDeleteProps)
{
    return (
        <div className={styles.card}>
            <h3>{room.roomNumber}</h3>
            <p>(Test only)ID: {room.id}</p>
            <p>{room.description}</p>
            <p>Number of beds: {room.numBeds}</p>
            <p>Daily Rate: {room.dailyRate}</p>

            <Link to={`/rooms/edit/${room.id}`}>Edit</Link>
            <button onClick={() => onDelete(room.id)}>Delete</button>
        </div>
    )
}