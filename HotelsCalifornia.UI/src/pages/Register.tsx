import { useEffect, useState } from "react"
import { useAuth } from "../context/AuthContext";
import { userService } from "../services/userService";
import type { Admin, Member, NewAdminDTO, NewMemberDTO } from "../types/User";




export default function Register() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [licenseNumber, setLicenseNumber] = useState("")
    const [email, setEmail] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const { user } = useAuth();
    const [loading, setLoading] = useState(false);

    const [error, setError] = useState<string | null>(null);

    const handleSubmitMember = async (e: React.FormEvent) => {
        e.preventDefault();
        setError("");
        setLoading(true);

        try {
            const newMember: NewMemberDTO = {
                username: username,
                passwordHash: password,
                licenseNumber: licenseNumber,
                email: email,
                phoneNumber: phoneNumber
            }
            const m: Member = await userService.postNewMember(newMember);

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
    const handleSubmitAdmin = async (e: React.FormEvent) => {
        e.preventDefault();
        setError("");
        setLoading(true);

        try {
            const newAdmin: NewAdminDTO = {
                username: username,
                passwordHash: password
            }
            const a: Admin = await userService.postNewAdmin(newAdmin);
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
            <h2>Register here!</h2>
            

            {user?.role !== 'Admin' && (
            <form onSubmit={handleSubmitMember}>
                <label>New Member</label>
                <div>
                    <label>UserName</label>
                    <input
                        type="text"
                        placeholder="Username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Password</label>
                    <input
                        type="password"
                        placeholder="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>License Number</label>
                    <input
                        type="text"
                        placeholder="License Number"
                        value={licenseNumber}
                        onChange={(e) => setLicenseNumber(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Email</label>
                    <input
                        type="text"
                        placeholder="Email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Phone Number</label>
                    <input
                        type="text"
                        placeholder="Phone Number"
                        value={phoneNumber}
                        onChange={(e) => setPhoneNumber(e.target.value)}
                        required
                    />
                </div>
                <button
                    type="submit"
                    disabled={loading}
                >
                    {loading ? "Submiting..." : "Submit"}
                </button>
            </form>
            )}
            {user?.role === 'Admin' && (
            <form onSubmit={handleSubmitAdmin}>
                <label>New Admin</label>
                <div>
                    <label>UserName</label>
                    <input
                        type="text"
                        placeholder="Username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Password</label>
                    <input
                        type="password"
                        placeholder="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <button
                    type="submit"
                    disabled={loading}
                >
                    {loading ? "Submiting..." : "Submit"}
                </button>
            </form>
       
        )}
        </div>
    )
}
