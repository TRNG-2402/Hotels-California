import { useState } from "react";
import { hotelService } from "../services/hotelService";
import { userService } from "../services/userService";

import type { NewHotelDTO } from "../types/Hotel";
import type { NewManagerDTO } from "../types/User";

export default function NewHotel() {

  const [username, setUsername] = useState("");

  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [address, setAddress] = useState("");

  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  const password = "ChangeMe";

  const handleSubmit = async (
    e: React.FormEvent<HTMLFormElement>
  ) => {

    e.preventDefault();

    setError("");
    setLoading(true);

    try {

      const newHotel: NewHotelDTO = {
        name,
        description,
        address
      };

      const hotel = await hotelService.postNewHotel(newHotel);

      const newManager: NewManagerDTO = {
        hotelId: hotel.id,
        username,
        passwordHash: password
      };

      await userService.postNewManager(newManager);

      alert("Hotel created successfully");

      setName("");
      setDescription("");
      setAddress("");
      setUsername("");

    } catch (err: any) {

      setError(
        err.response?.data?.message ||
        "Creation failed"
      );

    } finally {

      setLoading(false);
    }
  };

  return (
    
    <div style={{ padding: "20px" }}>
          <h2>New Hotel</h2>

         <div style={{ marginBottom: "20px" }}>
          <h3>Manager Details</h3>

          <div style={{ marginBottom: "10px" }}>
            <label>Username</label>
            <br />

            <input
              type="text"
              placeholder="Manager Username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              required
            />
          </div>
        </div>

        

    

      {error && (
        <p style={{ color: "red" }}>
          {error}
        </p>
      )}

      <form onSubmit={handleSubmit}>

        <div style={{ marginBottom: "20px" }}>
          <h3>Hotel Details</h3>

          <div style={{ marginBottom: "10px" }}>
            <label>Name</label>
            <br />

            <input
              type="text"
              placeholder="Hotel Name"
              value={name}
              onChange={(e) => setName(e.target.value)}
              required
            />
          </div>

          <div style={{ marginBottom: "10px" }}>
            <label>Description</label>
            <br />

            <input
              type="text"
              placeholder="Description"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              required
            />
          </div>

          <div style={{ marginBottom: "10px" }}>
            <label>Address</label>
            <br />

            <input
              type="text"
              placeholder="Address"
              value={address}
              onChange={(e) => setAddress(e.target.value)}
              required
            />
          </div>
        </div>

        <button
          type="submit"
          disabled={loading}
        >
          {loading
            ? "Submitting..."
            : "Create Hotel"}
        </button>

       

      </form>
    </div>
  );
}