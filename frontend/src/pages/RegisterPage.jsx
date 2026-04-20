import { useState } from 'react'
import { useMutation } from '@tanstack/react-query'
import { useNavigate, Link } from 'react-router-dom'
import { register } from '../services/api'

const CARD_SHADOW = 'rgba(0,0,0,0.02) 0px 0px 0px 1px, rgba(0,0,0,0.04) 0px 2px 6px, rgba(0,0,0,0.1) 0px 4px 8px'

export default function RegisterPage() {
  const navigate = useNavigate()
  const [form, setForm] = useState({ Email: '', Password: '' })
  const [error, setError] = useState('')

  const mutation = useMutation({
    mutationFn: register,
    onSuccess: () => navigate('/login'),
    onError: () => setError('Registration failed. Email may already be in use.'),
  })

  function handleSubmit(e) {
    e.preventDefault()
    setError('')
    mutation.mutate(form)
  }

  return (
    <div className="min-h-screen flex items-center justify-center px-4" style={{ background: '#ffffff' }}>
      <div
        className="bg-white w-full max-w-md p-8"
        style={{ borderRadius: '20px', boxShadow: CARD_SHADOW }}
      >
        <h1
          className="font-bold mb-6"
          style={{ color: '#222222', fontSize: '22px', letterSpacing: '-0.44px', lineHeight: '1.18' }}
        >
          Create your account
        </h1>

        {error && (
          <div
            className="text-sm p-3 mb-5 rounded-lg"
            style={{ color: '#c13515', background: '#fff5f5', border: '1px solid #fecaca' }}
          >
            {error}
          </div>
        )}

        <form onSubmit={handleSubmit} className="space-y-5">
          <div>
            <label className="block text-sm font-medium mb-1.5" style={{ color: '#222222' }}>
              Email
            </label>
            <input
              type="email"
              required
              value={form.Email}
              onChange={(e) => setForm({ ...form, Email: e.target.value })}
              className="w-full text-sm outline-none transition-shadow px-4 py-3 border"
              style={{
                color: '#222222',
                borderColor: '#c1c1c1',
                borderRadius: '8px',
              }}
              onFocus={e => (e.target.style.borderColor = '#ff385c')}
              onBlur={e => (e.target.style.borderColor = '#c1c1c1')}
            />
          </div>
          <div>
            <label className="block text-sm font-medium mb-1.5" style={{ color: '#222222' }}>
              Password
            </label>
            <input
              type="password"
              required
              value={form.Password}
              onChange={(e) => setForm({ ...form, Password: e.target.value })}
              className="w-full text-sm outline-none transition-shadow px-4 py-3 border"
              style={{
                color: '#222222',
                borderColor: '#c1c1c1',
                borderRadius: '8px',
              }}
              onFocus={e => (e.target.style.borderColor = '#ff385c')}
              onBlur={e => (e.target.style.borderColor = '#c1c1c1')}
            />
          </div>
          <button
            type="submit"
            disabled={mutation.isPending}
            className="w-full text-white font-medium text-base py-3 transition-colors disabled:opacity-50"
            style={{ background: '#222222', borderRadius: '8px' }}
            onMouseEnter={e => !mutation.isPending && (e.currentTarget.style.background = '#ff385c')}
            onMouseLeave={e => (e.currentTarget.style.background = '#222222')}
          >
            {mutation.isPending ? 'Creating account…' : 'Create account'}
          </button>
        </form>

        <p className="text-sm text-center mt-5" style={{ color: '#6a6a6a' }}>
          Already have an account?{' '}
          <Link to="/login" className="font-medium underline" style={{ color: '#222222' }}>
            Sign in
          </Link>
        </p>
      </div>
    </div>
  )
}
