import { useState } from 'react'
import { useMutation } from '@tanstack/react-query'
import { useNavigate, Link } from 'react-router-dom'
import { login } from '../services/api'

export default function LoginPage() {
  const navigate = useNavigate()
  const [form, setForm] = useState({ Email: '', Password: '' })
  const [error, setError] = useState('')

  const mutation = useMutation({
    mutationFn: login,
    onSuccess: (res) => {
      localStorage.setItem('token', res.data.token)
      navigate('/listings')
    },
    onError: () => setError('Invalid email or password.'),
  })

  function handleSubmit(e) {
    e.preventDefault()
    setError('')
    mutation.mutate(form)
  }

  return (
    <div className="min-h-screen bg-gray-50 flex items-center justify-center px-4">
      <div className="bg-white rounded-2xl shadow-sm border border-gray-200 p-8 w-full max-w-md">
        <h1 className="text-2xl font-bold text-gray-900 mb-6">Sign in</h1>

        {error && (
          <p className="text-red-600 text-sm bg-red-50 p-3 rounded-lg mb-4">
            {error}
          </p>
        )}

        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Email
            </label>
            <input
              type="email"
              required
              value={form.Email}
              onChange={(e) => setForm({ ...form, Email: e.target.value })}
              className="w-full border border-gray-300 rounded-lg px-4 py-2.5 focus:outline-none focus:ring-2 focus:ring-indigo-500"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">
              Password
            </label>
            <input
              type="password"
              required
              value={form.Password}
              onChange={(e) => setForm({ ...form, Password: e.target.value })}
              className="w-full border border-gray-300 rounded-lg px-4 py-2.5 focus:outline-none focus:ring-2 focus:ring-indigo-500"
            />
          </div>
          <button
            type="submit"
            disabled={mutation.isPending}
            className="w-full bg-indigo-600 text-white py-2.5 rounded-lg font-medium hover:bg-indigo-700 disabled:opacity-50"
          >
            {mutation.isPending ? 'Signing in...' : 'Sign in'}
          </button>
        </form>

        <p className="text-sm text-gray-500 mt-4 text-center">
          Don't have an account?{' '}
          <Link to="/register" className="text-indigo-600 hover:underline">
            Register
          </Link>
        </p>
      </div>
    </div>
  )
}
