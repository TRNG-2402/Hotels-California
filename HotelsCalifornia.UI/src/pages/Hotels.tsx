import HotelCard from "../components/HotelCard"
import type { Hotel } from "../types/Hotel"
import { useState, useEffect } from "react"
import EmptyState from "../components/EmptyState"
import { hotelService } from "../services/hotelService"
import SearchBar from "../components/SearchBar"
import { useAuth } from "../context/AuthContext"
import { Link } from "react-router-dom"

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

  if (isLoading) return <main><p>Loading Hotels...</p></main>

  if (error) return <main><p style={{ color: 'red' }}>{error}</p></main>

  const filtered = hotelList.filter((h) =>
    h.name.toLowerCase().includes(searchTerm.toLowerCase()));

  const handleDelete = async (id: number) => {
    await hotelService.deleteById(id);

    setHotelList((prev) => prev.filter((h) => h.id !== id));
  }
  return (
    <main>
      <h2>Search for hotels</h2>
      <SearchBar value={searchTerm} onSearchChange={setSearchTerm} />
      {user?.role === 'Admin' && (
        <Link to="/NewHotel">
          <button>
            New Hotel
          </button>
        </Link>
      )}
      {
        filtered.length === 0 ? (
          <EmptyState message={`No hotels match "${searchTerm}"`} />
        ) : (
          <section>
            {
              filtered.map((h) => (
                <div key={h.id}>
                  <HotelCard hotel={h} />

                  {user?.role === 'Admin' && (
                    <button
                      onClick={() => handleDelete(h.id)}
                      style={{ marginLeft: '0.5rem' }}
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
