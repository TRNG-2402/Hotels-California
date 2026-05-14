
import { Link, useNavigate } from "react-router-dom";
import styles from '../styles/NavBar.module.css'
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
        <nav className={styles.navbar}>
            <ul className={styles.navList}>
                <li><Link to="/" className={styles.link}>Home</Link></li>
                <li><Link to="/Hotels" className={styles.link}>Hotels</Link></li>
                <li><Link to="/Reservations" className={styles.link}>Reservations</Link></li>
                <li>
                    {
                        isAuthenticated ? (
                            <Link to="/Invoice" className={styles.link}>Invoice</Link>
                        ) : (<></>)
                    }
                    {
                        !isAuthenticated || user?.role === 'Admin' ? (
                            <Link to="/Register" className={styles.link}>Register</Link>
                        ) : (<></>)
                    }
                </li>
                <li>
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
                </li>
            </ul>

        </nav>
    )
}
