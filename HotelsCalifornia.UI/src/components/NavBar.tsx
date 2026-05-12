
import { Link, useNavigate } from "react-router-dom";
import styles from './NavBar.module.css'
import { useAuth } from '../context/AuthContext'

export default function NavBar()
{
    const { isAuthenticated, user, logout } = useAuth();
    const navigate = useNavigate();

    function handleLogout()
    {
        logout();
        navigate('/login');
    }

    return (
        <nav className={styles.css}>
            <Link to="/" className={styles.link}>Home</Link>
            <Link to="/Hotels" className={styles.link}></Link>
            <Link to="/Reservations" className={styles.link}></Link>
            {
                isAuthenticated ? (
                    <>
                        <span className={styles.greeting}>Hello, {user?.username}</span>
                        <button className={styles.linkButton} onClick={handleLogout}>
                            Log out
                        </button>
                    </>
                ) : (<Link to="/login" className={styles.link}>Login</Link>

                )
            }

        </nav>
    )
}