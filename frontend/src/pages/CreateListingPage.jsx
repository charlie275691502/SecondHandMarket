import { useState } from 'react'
import { useMutation } from '@tanstack/react-query'
import { useNavigate } from 'react-router-dom'
import { createListing } from '../services/api'

const INITIAL = { Title: '', Description: '', Price: '', Latitude: '', Longitude: '' }
const CARD_SHADOW = 'rgba(0,0,0,0.02) 0px 0px 0px 1px, rgba(0,0,0,0.04) 0px 2px 6px, rgba(0,0,0,0.1) 0px 4px 8px'

const inputStyle = {
  color: '#222222',
  borderColor: '#c1c1c1',
  borderRadius: '8px',
}

export default function CreateListingPage() {
  const navigate = useNavigate()
  const [form, setForm] = useState(INITIAL)
  const [error, setError] = useState('')

  const mutation = useMutation({
    mutationFn: createListing,
    onSuccess: () => navigate('/listings'),
    onError: () => setError('Failed to create listing. Please try again.'),
  })

  function handleSubmit(e) {
    e.preventDefault()
    setError('')
    mutation.mutate({
      ...form,
      Price: parseFloat(form.Price),
      Latitude: parseFloat(form.Latitude),
      Longitude: parseFloat(form.Longitude),
    })
  }

  function Field({ label, name, type = 'text', extra = {} }) {
    return (
      <div>
        <label className="block text-sm font-medium mb-1.5" style={{ color: '#222222' }}>
          {label}
        </label>
        <input
          type={type}
          required
          value={form[name]}
          onChange={(e) => setForm({ ...form, [name]: e.target.value })}
          className="w-full text-sm outline-none px-4 py-3 border"
          style={inputStyle}
          onFocus={e => (e.target.style.borderColor = '#ff385c')}
          onBlur={e => (e.target.style.borderColor = '#c1c1c1')}
          {...extra}
        />
      </div>
    )
  }

  return (
    <div className="min-h-screen flex items-center justify-center px-4 py-12" style={{ background: '#ffffff' }}>
      <div
        className="bg-white w-full max-w-lg p-8"
        style={{ borderRadius: '20px', boxShadow: CARD_SHADOW }}
      >
        <h1
          className="font-bold mb-6"
          style={{ color: '#222222', fontSize: '22px', letterSpacing: '-0.44px', lineHeight: '1.18' }}
        >
          Create a listing
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
          <Field label="Title" name="Title" />

          <div>
            <label className="block text-sm font-medium mb-1.5" style={{ color: '#222222' }}>
              Description
            </label>
            <textarea
              required
              rows={4}
              value={form.Description}
              onChange={(e) => setForm({ ...form, Description: e.target.value })}
              className="w-full text-sm outline-none px-4 py-3 border resize-none"
              style={inputStyle}
              onFocus={e => (e.target.style.borderColor = '#ff385c')}
              onBlur={e => (e.target.style.borderColor = '#c1c1c1')}
            />
          </div>

          <Field label="Price ($)" name="Price" type="number" extra={{ min: '0', step: '0.01' }} />

          <div className="grid grid-cols-2 gap-4">
            <Field label="Latitude" name="Latitude" type="number" extra={{ step: 'any' }} />
            <Field label="Longitude" name="Longitude" type="number" extra={{ step: 'any' }} />
          </div>

          <button
            type="submit"
            disabled={mutation.isPending}
            className="w-full text-white font-medium text-base py-3 transition-colors disabled:opacity-50 mt-2"
            style={{ background: '#222222', borderRadius: '8px' }}
            onMouseEnter={e => !mutation.isPending && (e.currentTarget.style.background = '#ff385c')}
            onMouseLeave={e => (e.currentTarget.style.background = '#222222')}
          >
            {mutation.isPending ? 'Publishing…' : 'Publish listing'}
          </button>
        </form>
      </div>
    </div>
  )
}
