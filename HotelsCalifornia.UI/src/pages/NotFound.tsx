import type { CSSProperties } from 'react'
import { Link } from 'react-router-dom'

export default function NotFound() {
  return (
    <section style={styles.container} aria-labelledby="not-found-title">
      <p style={styles.status}>404</p>
      <h2 id="not-found-title" style={styles.title}>
        Page not found
      </h2>
      <p style={styles.message}>
        The page you are trying to access does not exist. Try searching for something else.
      </p>
      <div style={styles.actions}>
        <Link style={styles.primaryLink} to="/">
          Go Back
        </Link>
      </div>
    </section>
  )
}

const styles = {
  container: {
    display: 'flex',
    minHeight: '50svh',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
    gap: '14px',
    padding: '48px 20px',
  },
  status: {
    color: 'var(--accent)',
    fontFamily: 'var(--mono)',
    width: 'clamp(180px, 42vw, 380px)',
    fontSize: 'clamp(70px, 16vw, 210px)',
    fontWeight: 500,
    lineHeight: 0.5,
    textAlign: 'center',
  },
  title: {
    width: 'clamp(180px, 42vw, 380px)',
    fontSize: 'clamp(24px, 5vw, 52px)',
    lineHeight: 1.05,
    marginTop: '12px',
  },
  message: {
    maxWidth: '560px',
  },
  actions: {
    display: 'flex',
    flexWrap: 'wrap',
    justifyContent: 'center',
    gap: '12px',
    marginTop: '12px',
  },
  primaryLink: {
    borderRadius: '6px',
    background: 'var(--accent)',
    color: 'var(--bg)',
    padding: '10px 16px',
    textDecoration: 'none',
  },
} satisfies Record<string, CSSProperties>
