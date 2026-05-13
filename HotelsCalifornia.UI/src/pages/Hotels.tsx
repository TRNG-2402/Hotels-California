import { useState } from "react";
import SearchBar from "../components/SearchBar";
import type { Hotel } from "../types/Hotel";
import styles from "../styles/Hotels.module.css";

export default function Hotels() {

  const [search, setSearch] = useState("");

  const hotels: Hotel[] = [
  {
    hotelId: 1,
    name: "Sunset Pacific Resort",
    description: "Beachfront resort with ocean views, spa services, and fine dining.",
    address: "Santa Monica, California"
  },
  {
    hotelId: 2,
    name: "Golden Gate Grand Hotel",
    description: "Modern luxury hotel near downtown with rooftop lounge and bay views.",
    address: "San Francisco, California"
  },
  {
    hotelId: 3,
    name: "Palm Oasis Retreat",
    description: "Relaxing desert escape featuring pools, cabanas, and wellness programs.",
    address: "Palm Springs, California"
  },
  {
    hotelId: 4,
    name: "Hollywood Star Suites",
    description: "Boutique hotel located near Hollywood attractions with premium suites.",
    address: "Los Angeles, California"
  },
  {
    hotelId: 5,
    name: "Yosemite Valley Lodge",
    description: "Nature-focused lodge offering scenic views and outdoor adventure access.",
    address: "Yosemite National Park, California"
  }
  ];


  const filteredHotels = hotels.filter((hotel) =>
    hotel.name.toLowerCase().includes(search.toLowerCase()) ||
    hotel.address.toLowerCase().includes(search.toLowerCase())
  );

  return (
    <div className={styles.container}>

      <div className={styles.header}>
        <h1>Find Your Perfect Hotel</h1><br/>

        <p>
          Search hotels by city or hotel name
        </p>
      </div>

      <SearchBar
        value={search}
        onSearchChange={setSearch}
      />

      <div className={styles.hotelGrid}>

        {filteredHotels.map((hotel) => (
          <div
            key={hotel.hotelId}
            className={styles.hotelCard}
          >

            <div className={styles.hotelImage}>
              Hotel Image
            </div>

            <h2>{hotel.name}</h2>

            <p className={styles.description}>
              {hotel.description}
            </p>

            <p className={styles.address}>
              {hotel.address}
            </p>

            <button className={styles.reserveButton}>
              Reserve Now
            </button>

          </div>
        ))}

      </div>
    </div>
  );
}