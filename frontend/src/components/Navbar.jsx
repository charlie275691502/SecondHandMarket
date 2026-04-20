import { Link, useNavigate } from 'react-router-dom'
import { useAuth } from '../hooks/useAuth'

export default function Navbar() {
  const { isAuthenticated } = useAuth()
  const navigate = useNavigate()

  function handleLogout() {
    localStorage.removeItem('token')
    navigate('/login')
  }

  return (
    <nav
      className="sticky top-0 z-50 bg-white flex items-center justify-between px-6 py-4"
      style={{ borderBottom: '1px solid #e8e8e8' }}
    >
      <Link
        to="/listings"
        className="font-bold text-xl tracking-tight"
        style={{ color: '#ff385c', letterSpacing: '-0.44px' }}
      >
        SecondHand Market
      </Link>

      <div className="flex items-center gap-4">
        {isAuthenticated ? (
          <>
            <Link
              to="/listings"
              className="text-sm font-medium transition-colors"
              style={{ color: '#222222' }}
              onMouseEnter={e => (e.target.style.color = '#6a6a6a')}
              onMouseLeave={e => (e.target.style.color = '#222222')}
            >
              Listings
            </Link>
            <Link
              to="/create-listing"
              className="text-sm font-medium text-white px-6 py-2.5 rounded-lg transition-colors"
              style={{ background: '#222222', borderRadius: '8px' }}
              onMouseEnter={e => (e.currentTarget.style.background = '#ff385c')}
              onMouseLeave={e => (e.currentTarget.style.background = '#222222')}
            >
              + Sell
            </Link>
            <button
              onClick={handleLogout}
              className="text-sm transition-colors"
              style={{ color: '#6a6a6a' }}
              onMouseEnter={e => (e.target.style.color = '#222222')}
              onMouseLeave={e => (e.target.style.color = '#6a6a6a')}
            >
              Logout
            </button>
          </>
        ) : (
          <>
            <Link
              to="/login"
              className="text-sm font-medium transition-colors"
              style={{ color: '#222222' }}
              onMouseEnter={e => (e.target.style.color = '#6a6a6a')}
              onMouseLeave={e => (e.target.style.color = '#222222')}
            >
              Login
            </Link>
            <Link
              to="/register"
              className="text-sm font-medium text-white px-6 py-2.5 transition-colors"
              style={{ background: '#222222', borderRadius: '8px' }}
              onMouseEnter={e => (e.currentTarget.style.background = '#ff385c')}
              onMouseLeave={e => (e.currentTarget.style.background = '#222222')}
            >
              Register
            </Link>
          </>
        )}
      </div>
    </nav>
  )
}
