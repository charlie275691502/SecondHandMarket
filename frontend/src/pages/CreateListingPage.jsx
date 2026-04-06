import { useState } from 'react'
import { useMutation } from '@tanstack/react-query'
import { useNavigate } from 'react-router-dom'
import { createListing } from '../services/api'

const INITIAL = { Title: '', Description: '', Price: '', Latitude: '', Longitude: '' }

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

  function field(label, name, type = 'text', extra = {}) {
    return (
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">{label}</label>
        <input
          type={type}
          required
          value={form[name]}
          onChange={(e) => setForm({ ...form, [name]: e.target.value })}
          className="w-full border border-gray-300 rounded-lg px-4 py-2.5 focus:outline-none focus:ring-2 focus:ring-indigo-500"
          {...extra}
        />
      </div>
    )
  }

  return (
    <div className="min-h-screen bg-gray-50 flex items-center justify-center px-4">
      <div className="bg-white rounded-2xl shadow-sm border border-gray-200 p-8 w-full max-w-lg">
        <h1 className="text-2xl font-bold text-gray-900 mb-6">Create Listing</h1>

        {error && (
          <p className="text-red-600 text-sm bg-red-50 p-3 rounded-lg mb-4">{error}</p>
        )}

        <form onSubmit={handleSubmit} className="space-y-4">
          {field('Title', 'Title')}
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">Description</label>
            <textarea
              required
              rows={3}
              value={form.Description}
              onChange={(e) => setForm({ ...form, Description: e.target.value })}
              className="w-full border border-gray-300 rounded-lg px-4 py-2.5 focus:outline-none focus:ring-2 focus:ring-indigo-500 resize-none"
            />
          </div>
          {field('Price ($)', 'Price', 'number', { min: '0', step: '0.01' })}
          <div className="grid grid-cols-2 gap-4">
            {field('Latitude', 'Latitude', 'number', { step: 'any' })}
            {field('Longitude', 'Longitude', 'number', { step: 'any' })}
          </div>
          <button
            type="submit"
            disabled={mutation.isPending}
            className="w-full bg-indigo-600 text-white py-2.5 rounded-lg font-medium hover:bg-indigo-700 disabled:opacity-50 mt-2"
          >
            {mutation.isPending ? 'Publishing...' : 'Publish Listing'}
          </button>
        </form>
      </div>
    </div>
  )
}
