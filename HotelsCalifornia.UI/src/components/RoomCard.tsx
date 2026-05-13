import type { Room } from "../types/Room"
//TODO make own styles for room
import styles from '../styles/HotelCard.module.css'
import { Link } from "react-router-dom"


interface RoomCardProps
{
    room: Room
}
export default function RoomCard({ room }: RoomCardProps)
{
    return (
        <div className={styles.card}>
            <h3>{room.roomNumber}</h3>
            <p>{room.description}</p>
            <p>Number of beds: {room.numBeds}</p>
            <p>Daily Rate: {room.dailyRate}</p>
        </div>
    )
}
