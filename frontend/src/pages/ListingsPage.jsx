import { useState } from 'react'
import { useQuery } from '@tanstack/react-query'
import { getListings } from '../services/api'
import ListingCard from '../components/ListingCard'

export default function ListingsPage() {
  const [search, setSearch] = useState({ latitude: '', longitude: '', radiusKm: '' })
  const [params, setParams] = useState({})

  const { data, isLoading, isError } = useQuery({
    queryKey: ['listings', params],
    queryFn: () => getListings(params).then((r) => r.data),
  })

  function handleSearch(e) {
    e.preventDefault()
    const { latitude, longitude, radiusKm } = search
    if (latitude && longitude && radiusKm) {
      setParams({ latitude, longitude, radiusKm })
    } else {
      setParams({})
    }
  }

  const listings = Array.isArray(data) ? data : []

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="max-w-5xl mx-auto px-4 py-8">
        <h1 className="text-3xl font-bold text-gray-900 mb-6">Listings</h1>

        {/* Search bar */}
        <form
          onSubmit={handleSearch}
          className="bg-white border border-gray-200 rounded-xl p-4 mb-8 flex flex-wrap gap-3 items-end"
        >
          <div>
            <label className="block text-xs font-medium text-gray-600 mb-1">Latitude</label>
            <input
              type="number"
              step="any"
              placeholder="e.g. 23.5"
              value={search.latitude}
              onChange={(e) => setSearch({ ...search, latitude: e.target.value })}
              className="border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500 w-32"
            />
          </div>
          <div>
            <label className="block text-xs font-medium text-gray-600 mb-1">Longitude</label>
            <input
              type="number"
              step="any"
              placeholder="e.g. 15.0"
              value={search.longitude}
              onChange={(e) => setSearch({ ...search, longitude: e.target.value })}
              className="border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500 w-32"
            />
          </div>
          <div>
            <label className="block text-xs font-medium text-gray-600 mb-1">Radius (km)</label>
            <input
              type="number"
              placeholder="e.g. 100"
              value={search.radiusKm}
              onChange={(e) => setSearch({ ...search, radiusKm: e.target.value })}
              className="border border-gray-300 rounded-lg px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500 w-28"
            />
          </div>
          <button
            type="submit"
            className="bg-indigo-600 text-white px-5 py-2 rounded-lg text-sm font-medium hover:bg-indigo-700"
          >
            Search
          </button>
          {Object.keys(params).length > 0 && (
            <button
              type="button"
              onClick={() => { setParams({}); setSearch({ latitude: '', longitude: '', radiusKm: '' }) }}
              className="text-gray-500 text-sm hover:text-gray-800"
            >
              Clear
            </button>
          )}
        </form>

        {/* Results */}
        {isLoading && (
          <p className="text-gray-500 text-center py-12">Loading listings...</p>
        )}
        {isError && (
          <p className="text-red-500 text-center py-12">Failed to load listings.</p>
        )}
        {!isLoading && !isError && listings.length === 0 && (
          <p className="text-gray-400 text-center py-12">No listings found.</p>
        )}
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
          {listings.map((listing) => (
            <ListingCard key={listing.id} listing={listing} />
          ))}
        </div>
      </div>
    </div>
  )
}
