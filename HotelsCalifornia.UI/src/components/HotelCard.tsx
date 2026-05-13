
import type { Hotel } from '../types/Hotel';
import styles from '../styles/HotelCard.module.css'
import { Link } from 'react-router-dom';

interface HotelCardProps {
    hotel: Hotel;
}

export default function HotelCard({ hotel }: HotelCardProps) {

    return (
        <div className={styles.card}>
            <h3>{hotel.name}</h3>
            <p>{hotel.description ?? 'No description'}</p><br/>
            <p><strong>{hotel.address}</strong></p>
        </div>

    )
}