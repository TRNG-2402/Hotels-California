import HotelCard from "../componets/HotelCard"
import type { Hotel } from "../types/Hotel"
import { useState, useEffect } from "react"
import EmptyState from "../componets/EmptyState"
import { hotelService } from "../services/hotelService"
import SearchBar from "../componets/SearchBar"

export default function Hotels() {

  const [searchTerm, setSearchTerm] = useState('');

  const [hotelList, setHotelList] = useState<Hotel[]>([]);

  const [isLoading,setIsLoading] = useState(true);

  const [error, setError] = useState<string | null>(null);

  useEffect(() => {

    hotelService.getAllHotels()
      .then((data) => setHotelList(data))
      .catch((err) => setError(err.message ?? 'Failed to load hotel'))
      .finally(() => setIsLoading(false))
  })

  if (isLoading) return <main><p>Loading Hotels...</p></main>

  if (error) return <main><p style={{color : 'red'}}>{error}</p></main>

  const filtered = hotelList.filter((h) =>
  h.name.toLowerCase().includes(searchTerm.toLowerCase())
);


  return (
    <main>
      <p>hello</p>
      <SearchBar value={searchTerm} onSearchChange={setSearchTerm} />
      {
        filtered.length === 0 ? (
          <EmptyState message={`No hotels match "${searchTerm}"`} />
        ) : (
          <section>
        {
          
            filtered.map((h) => (
              <HotelCard key={h.hotelId} hotel={h} />
            ))}
          </section>
        )}
    </main>
  )
}
