
import { useState } from "react"
import { useAuth } from "../context/AuthContext"
import { hotelService } from "../services/hotelService";
import type { Hotel, NewHotelDTO } from "../types/Hotel";
import type { Manager, NewManagerDTO } from "../types/User";
import { userService } from "../services/userService";

export default function NewHotel() {
    const [username, setUsername] = useState("");
    const [password] = useState("ChangeMe");


    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [address, setAddress] = useState("");
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError("");
        setLoading(true);

        try {
            const newHotel: NewHotelDTO = {
                name: name,
                description: description,
                address: address
            }
            const h: Hotel = await hotelService.postNewHotel(newHotel);

            const newManager: NewManagerDTO = {
                hotelId: h.id,
                username: username,
                passwordHash: password
            }
            const m : Manager = await userService.postNewManager(newManager);
        }
        catch (err: any) {
            setError(
                err.response?.data?.message ||
                "Creation failed"
            );
        }
        finally {
            setLoading(false);
        }
    }
    return (
        <div>
            <h2>New Hotel</h2>

            <form onSubmit={handleSubmit}>
                <div>
                    <label>Hotel Details</label>
                    <div>
                        <label>Name</label>
                        <input
                            type="text"
                            placeholder="Hotel Name"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            required
                        />
                    </div>
                    <div>
                        <label>Description</label>
                        <input
                            type="text"
                            placeholder="Description"
                            value={description}
                            onChange={(e) => setDescription(e.target.value)}
                            required
                        />
                    </div>
                    <div>
                        <label>Address</label>
                        <input
                            type="text"
                            placeholder="Address"
                            value={address}
                            onChange={(e) => setAddress(e.target.value)}
                            required
                        />
                    </div>
                </div>
                <div>
                    <label>Manager Details</label>
                    <div>
                        <label>Username</label>
                        <input
                            type="text"
                            placeholder="Manager Username"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                            required
                        />
                    </div>
                </div>
                <button
                    type="submit"
                    disabled={loading}
                >
                    {loading ? "Submiting..." : "Submit"}
                </button>
            </form>
        </div>
    )
}
