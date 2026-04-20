import { useState } from 'react'
import { useQuery } from '@tanstack/react-query'
import { getListings } from '../services/api'
import ListingCard from '../components/ListingCard'

const CARD_SHADOW = 'rgba(0,0,0,0.02) 0px 0px 0px 1px, rgba(0,0,0,0.04) 0px 2px 6px, rgba(0,0,0,0.1) 0px 4px 8px'

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
    <div className="min-h-screen" style={{ background: '#ffffff' }}>
      <div className="max-w-7xl mx-auto px-6 py-10">

        {/* Page heading */}
        <h1
          className="font-bold mb-8"
          style={{ color: '#222222', fontSize: '28px', lineHeight: '1.43' }}
        >
          Find something great
        </h1>

        {/* Search bar */}
        <form
          onSubmit={handleSearch}
          className="bg-white flex flex-wrap items-end gap-4 mb-10 p-5"
          style={{ borderRadius: '32px', boxShadow: CARD_SHADOW }}
        >
          <div className="flex-1 min-w-[120px]">
            <label
              className="block text-xs font-semibold mb-1"
              style={{ color: '#222222' }}
            >
              Latitude
            </label>
            <input
              type="number"
              step="any"
              placeholder="e.g. 23.5"
              value={search.latitude}
              onChange={(e) => setSearch({ ...search, latitude: e.target.value })}
              className="w-full text-sm outline-none bg-transparent"
              style={{ color: '#222222', borderBottom: '1px solid #c1c1c1', padding: '6px 0' }}
            />
          </div>
          <div className="flex-1 min-w-[120px]">
            <label
              className="block text-xs font-semibold mb-1"
              style={{ color: '#222222' }}
            >
              Longitude
            </label>
            <input
              type="number"
              step="any"
              placeholder="e.g. 15.0"
              value={search.longitude}
              onChange={(e) => setSearch({ ...search, longitude: e.target.value })}
              className="w-full text-sm outline-none bg-transparent"
              style={{ color: '#222222', borderBottom: '1px solid #c1c1c1', padding: '6px 0' }}
            />
          </div>
          <div className="flex-1 min-w-[100px]">
            <label
              className="block text-xs font-semibold mb-1"
              style={{ color: '#222222' }}
            >
              Radius (km)
            </label>
            <input
              type="number"
              placeholder="e.g. 100"
              value={search.radiusKm}
              onChange={(e) => setSearch({ ...search, radiusKm: e.target.value })}
              className="w-full text-sm outline-none bg-transparent"
              style={{ color: '#222222', borderBottom: '1px solid #c1c1c1', padding: '6px 0' }}
            />
          </div>
          <div className="flex items-center gap-3">
            <button
              type="submit"
              className="flex items-center justify-center text-white font-medium text-sm transition-colors"
              style={{
                background: '#ff385c',
                borderRadius: '50%',
                width: '48px',
                height: '48px',
                flexShrink: 0,
              }}
              onMouseEnter={e => (e.currentTarget.style.background = '#e00b41')}
              onMouseLeave={e => (e.currentTarget.style.background = '#ff385c')}
            >
              <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="white" strokeWidth="2.5" strokeLinecap="round" strokeLinejoin="round">
                <circle cx="11" cy="11" r="8" />
                <path d="m21 21-4.35-4.35" />
              </svg>
            </button>
            {Object.keys(params).length > 0 && (
              <button
                type="button"
                onClick={() => { setParams({}); setSearch({ latitude: '', longitude: '', radiusKm: '' }) }}
                className="text-sm underline"
                style={{ color: '#6a6a6a' }}
              >
                Clear
              </button>
            )}
          </div>
        </form>

        {/* States */}
        {isLoading && (
          <p className="text-center py-16 text-sm" style={{ color: '#6a6a6a' }}>
            Loading listings…
          </p>
        )}
        {isError && (
          <p className="text-center py-16 text-sm" style={{ color: '#c13515' }}>
            Failed to load listings.
          </p>
        )}
        {!isLoading && !isError && listings.length === 0 && (
          <p className="text-center py-16 text-sm" style={{ color: '#6a6a6a' }}>
            No listings found.
          </p>
        )}

        {/* Grid */}
        <div className="grid gap-6 grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-5">
          {listings.map((listing) => (
            <ListingCard key={listing.id} listing={listing} />
          ))}
        </div>
      </div>
    </div>
  )
}
