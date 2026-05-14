import { useState, type FormEvent } from "react";
import { Link } from "react-router-dom";
import { api } from "../services/api";
import { useAuth } from "../context/AuthContext";
import type { UpdateUserDTO } from "../types/User";
import "../styles/Login.css";

export default function ResetPassword()
{
    const { user, isAuthenticated } = useAuth();
    const [newPassword, setNewPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [message, setMessage] = useState("");
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);

    async function handleSubmit(e: FormEvent<HTMLFormElement>)
    {
        e.preventDefault();
        setMessage("");
        setError("");

        if (!user)
        {
            setError("You must be logged in to reset your password.");
            return;
        }

        if (newPassword !== confirmPassword)
        {
            setError("Passwords do not match.");
            return;
        }

        const updatedUser: UpdateUserDTO = {
            id: user.userId,
            username: user.username,
            passwordHash: newPassword
        };

        try
        {
            setLoading(true);
            await api.patch("/User", updatedUser);
            setNewPassword("");
            setConfirmPassword("");
            setMessage("Password updated successfully.");
        }
        catch
        {
            setError("Password reset failed. Please try again.");
        }
        finally
        {
            setLoading(false);
        }
    }

    if (!isAuthenticated)
    {
        return (
            <div className="login-container">
                <div className="login-card">
                    <div className="login-header">
                        <h1>Reset Password</h1>
                    </div>

                    <p style={{ marginBottom: "20px" }}>
                        Please log in before resetting your password.
                    </p>

                    <Link to="/login" className="login-button" style={{ display: "block", textAlign: "center", textDecoration: "none" }}>
                        Login
                    </Link>
                </div>
            </div>
        );
    }

    return (
        <div className="login-container">
            <div className="login-card">
                <div className="login-header">
                    <h1>Reset Password</h1>
                </div>

                {message && (
                    <div style={{ color: "#2e7d32", marginBottom: "1rem", textAlign: "center" }}>
                        {message}
                    </div>
                )}

                {error && (
                    <div style={{ color: "#d32f2f", marginBottom: "1rem", textAlign: "center" }}>
                        {error}
                    </div>
                )}

                <form className="login-form" onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label>New Password</label>
                        <input
                            type="password"
                            placeholder="Enter new password"
                            value={newPassword}
                            onChange={(e) => setNewPassword(e.target.value)}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>Confirm Password</label>
                        <input
                            type="password"
                            placeholder="Confirm new password"
                            value={confirmPassword}
                            onChange={(e) => setConfirmPassword(e.target.value)}
                            required
                        />
                    </div>

                    <button
                        type="submit"
                        className="login-button"
                        disabled={loading}
                    >
                        {loading ? "Updating..." : "Reset Password"}
                    </button>
                </form>
            </div>
        </div>
    );
}
