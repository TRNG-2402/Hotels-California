
import type { Hotel } from '../types/Hotel';
import styles from '../styles/HotelCard.module.css'

interface HotelCardProps {
    hotel: Hotel;
}

export default function HotelCard({ hotel }: HotelCardProps) {

    return (
        <div className={styles.card}>
            <h3 className={styles.name}>{hotel.name}</h3>
            <p className={styles.address}>{hotel.address}</p>
            <p className={styles.description}>{hotel.description ?? 'No description'}</p>
        </div>

    )
}
