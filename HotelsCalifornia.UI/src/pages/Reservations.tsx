import { useEffect, useState } from "react";
import type { Reservation } from "../types/Reservation";
import ReservationCard from "../components/ReservationCard";
import { reservationService } from "../services/reservationService";
import styles from "../styles/Reservations.module.css";

export default function Reservations() {
  const [reservations, setReservations] = useState<Reservation[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    let isMounted = true;

    async function loadReservations() {
      try {
        setIsLoading(true);
        setError(null);

        const results = await reservationService.getAllReservations();

        if (isMounted) {
          console.log(results);

          setReservations(results);
        }
      }
      catch {
        if (isMounted) {
          setError("Unable to load reservations.");
        }
      }
      finally {
        if (isMounted) {
          setIsLoading(false);
        }
      }
    }

    loadReservations();

    return () => {
      isMounted = false;
    };
  }, []);

  return (
    <div className={styles.reservationContainer}>
      <h1 className={styles.title}>Reservations</h1>

      {isLoading && <p className={styles.message}>Loading reservations...</p>}
      {error && <p className={styles.error}>{error}</p>}

      {!isLoading && !error && reservations.length === 0 && (
        <p className={styles.message}>No reservations found.</p>
      )}

      {!isLoading && !error && reservations.length > 0 && (
        <div className={styles.reservationList}>
          {reservations.map((reservation) => (
            <ReservationCard
              key={reservation.reservationId}
              reservation={reservation}
            />
          ))}
        </div>
      )}
    </div>
  );
}
