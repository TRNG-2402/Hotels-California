import { useEffect, useState } from 'react';
import type { Reservation } from '../types/Reservation';
import type { Hotel } from '../types/Hotel';
import type { Room } from '../types/Room';
import { hotelService } from '../services/hotelService';
import { roomService } from '../services/roomService';
import { reservationService } from '../services/reservationService';
import styles from '../styles/ReservationCard.module.css';

interface ReservationCardProps {
    reservation: Reservation;
}

function formatDate(value: Date | string | null | undefined) {
    if (!value) {
        return 'Not set';
    }

    return new Date(value).toLocaleString(undefined, {
        year: 'numeric',
        month: 'numeric',
        day: 'numeric',
        hour: 'numeric',
        minute: '2-digit'
    });
}

export default function ReservationCard({ reservation }: ReservationCardProps) {
    const [hotel, setHotel] = useState<Hotel | null>(null);
    const [room, setRoom] = useState<Room | null>(null);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        let isMounted = true;

        async function loadReservationDetails() {
            try {
                setError(null);
                setHotel(null);
                setRoom(null);

                const [hotelResult, roomResult] = await Promise.all([
                    hotelService.getHotelById(reservation.hotelId),
                    roomService.getRoomById(reservation.roomId)
                ]);

                if (!isMounted) {
                    return;
                }

                setHotel(hotelResult);
                setRoom(roomResult);
            }
            catch {
                if (isMounted) {
                    setHotel(null);
                    setRoom(null);
                    setError('Unable to load reservation details.');
                }
            }
        }

        loadReservationDetails();

        return () => {
            isMounted = false;
        };
    }, [reservation.hotelId, reservation.roomId, reservation.isCanceled]);

    function cancelReservation(id: number) {
        reservationService.updateReservation({
            reservationId: id,
            isCanceled: true,
            checkOutTime: null,
            driversLicense: null,
            email: null,
            phoneNumber: null
        });
        reservation.isCanceled = true;
    }

    return (
        <div className={styles.card}>
            <div className={styles.header}>
                <h3>{hotel ? hotel.name : 'Loading hotel...'}</h3>
                <span className={reservation.isCanceled ? styles.canceled : styles.active}>
                    {reservation.isCanceled ? 'Canceled' : 'Active'}
                </span>
            </div>

            {error && <p className={styles.error}>{error}</p>}

            <div className={styles.details}>
                <div>
                    <span className={styles.label}>Room</span>
                    <p>{room ? `Room ${room.roomNumber}` : 'Loading room...'}</p>
                </div>

                <div>
                    <span className={styles.label}>Room Info</span>
                    <p>{room ? `${room.numBeds} beds` : 'Loading...'}</p>
                    {room && <small>${room.dailyRate}/night</small>}
                </div>

                <div>
                    <span className={styles.label}>Check In</span>
                    <p>{formatDate(reservation.checkInTime)}</p>
                </div>

                <div>
                    <span className={styles.label}>Check Out</span>
                    <p>{formatDate(reservation.checkOutTime)}</p>
                </div>

                <br />
                <button 
                onClick={() => cancelReservation(reservation.reservationId)}>
                Cancel
                </button>

            </div>
        </div>
    );
}
