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
    <nav className="bg-white border-b border-gray-200 px-6 py-4 flex items-center justify-between">
      <Link to="/listings" className="text-xl font-bold text-indigo-600">
        SecondHand Market
      </Link>
      <div className="flex gap-4 items-center">
        {isAuthenticated ? (
          <>
            <Link to="/listings" className="text-gray-600 hover:text-gray-900">
              Listings
            </Link>
            <Link
              to="/create-listing"
              className="bg-indigo-600 text-white px-4 py-2 rounded-lg hover:bg-indigo-700"
            >
              + Sell
            </Link>
            <button
              onClick={handleLogout}
              className="text-gray-500 hover:text-gray-800 text-sm"
            >
              Logout
            </button>
          </>
        ) : (
          <>
            <Link to="/login" className="text-gray-600 hover:text-gray-900">
              Login
            </Link>
            <Link
              to="/register"
              className="bg-indigo-600 text-white px-4 py-2 rounded-lg hover:bg-indigo-700"
            >
              Register
            </Link>
          </>
        )}
      </div>
    </nav>
  )
}
