import HotelCard from "../components/HotelCard"
import type { Hotel } from "../types/Hotel"
import { useState, useEffect } from "react"
import EmptyState from "../components/EmptyState"
import { hotelService } from "../services/hotelService"
import SearchBar from "../components/SearchBar"
import { useAuth } from "../context/AuthContext"
import { Link } from "react-router-dom"
import styles from "../styles/Hotels.module.css"

export default function Hotels() {

  const { user } = useAuth();

  const [hotelList, setHotelList] = useState<Hotel[]>([]);

  const [searchTerm, setSearchTerm] = useState('');

  const [isLoading, setIsLoading] = useState(true);

  const [error, setError] = useState<string | null>(null);

  useEffect(() => {

    hotelService.getAllHotels()
      .then((data) => setHotelList(data))
      .catch((err) => setError(err.message ?? 'Failed to load hotel'))
      .finally(() => setIsLoading(false))
  }, [])

  if (isLoading) return <main className={styles.container}><p className={styles.message}>Loading Hotels...</p></main>

  if (error) return <main className={styles.container}><p className={styles.error}>{error}</p></main>

  const filtered = hotelList.filter((h) =>
    h.name.toLowerCase().includes(searchTerm.toLowerCase()));

  const handleDelete = async (id: number) => {
    await hotelService.deleteById(id);

    setHotelList((prev) => prev.filter((h) => h.id !== id));
  }
  return (
    <main className={styles.container}>
      <h2 className={styles.title}>Search for hotels</h2>
      <div className={styles.controls}>
        <div className={styles.searchBarWrapper}>
          <SearchBar value={searchTerm} onSearchChange={setSearchTerm} />
        </div>
        {user?.role === 'Admin' && (
          <Link to="/NewHotel">
            <button>
              New Hotel
            </button>
          </Link>
        )}
      </div>
      {
        filtered.length === 0 ? (
          <EmptyState message={`No hotels match "${searchTerm}"`} />
        ) : (
          <section className={styles.hotelList}>
            {
              filtered.map((h) => (
                <div className={styles.hotelItem} key={h.id}>
                  <Link className={styles.hotelLink} to={`/hotels/${h.id}/rooms`}>
                    <HotelCard hotel={h} />
                  </Link>
                  {user?.role === 'Admin' && (
                    <button
                      onClick={() => handleDelete(h.id)}
                      className={styles.deleteButton}
                    >
                      Delete
                    </button>
                  )}
                </div>
              ))
            }

          </section>
        )}
    </main>
  )
}
