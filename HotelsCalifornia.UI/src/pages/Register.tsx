import { useState } from "react";
import { useAuth } from "../context/AuthContext";
import { userService } from "../services/userService";
import type {
  NewAdminDTO,
  NewMemberDTO
} from "../types/User";

export default function Register() {

  const { user } = useAuth();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [licenseNumber, setLicenseNumber] = useState("");
  const [email, setEmail] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");

  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const isAdmin = user?.role === "Admin";

  const inputStyle = {
    width: "100%",
    padding: "10px",
    marginTop: "5px",
    marginBottom: "15px"
  };

  const buttonStyle = {
    width: "100%",
    padding: "12px",
    backgroundColor: "#aa3bff",
    color: "white",
    borderRadius: "6px",
    cursor: "pointer"
  };

  const clearForm = () => {
    setUsername("");
    setPassword("");
    setLicenseNumber("");
    setEmail("");
    setPhoneNumber("");
  };

  const handleSubmit = async (
    e: React.FormEvent<HTMLFormElement>
  ) => {

    e.preventDefault();

    setError(null);
    setLoading(true);

    try {

      if (isAdmin) {

        const newAdmin: NewAdminDTO = {
          username,
          passwordHash: password
        };

        await userService.postNewAdmin(newAdmin);

        alert("Admin created successfully");

      } else {

        const newMember: NewMemberDTO = {
          username,
          passwordHash: password,
          licenseNumber,
          email,
          phoneNumber
        };

        await userService.postNewMember(newMember);

        alert("Member created successfully");
      }

      clearForm();

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
    <div
      style={{
        maxWidth: "500px",
        margin: "40px auto",
        padding: "30px",
        borderRadius: "12px",
        boxShadow: "0 4px 10px rgba(0,0,0,0.1)"
      }}
    >

      <h2 style={{ textAlign: "center" }}>
        {isAdmin ? "Create Admin" : "Member Registration"}
      </h2>

      {error && (
        <p style={{ color: "red" }}>
          {error}
        </p>
      )}

      <form onSubmit={handleSubmit}>

        <label>Username</label>
        <input
          type="text"
          placeholder="Username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          required
          style={inputStyle}
        />

        <label>Password</label>
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
          style={inputStyle}
        />

        {!isAdmin && (
          <>
            <label>License Number</label>
            <input
              type="text"
              placeholder="License Number"
              value={licenseNumber}
              onChange={(e) => setLicenseNumber(e.target.value)}
              required
              style={inputStyle}
            />

            <label>Email</label>
            <input
              type="email"
              placeholder="Email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              style={inputStyle}
            />

            <label>Phone Number</label>
            <input
              type="text"
              placeholder="Phone Number"
              value={phoneNumber}
              onChange={(e) => setPhoneNumber(e.target.value)}
              required
              style={inputStyle}
            />
          </>
        )}

        <button
          type="submit"
          disabled={loading}
          style={buttonStyle}
        >
          {loading
            ? "Submitting..."
            : isAdmin
              ? "Create Admin"
              : "Register"}
        </button>

      </form>
    </div>
  );
}