const CARD_SHADOW = 'rgba(0,0,0,0.02) 0px 0px 0px 1px, rgba(0,0,0,0.04) 0px 2px 6px, rgba(0,0,0,0.1) 0px 4px 8px'
const HOVER_SHADOW = 'rgba(0,0,0,0.08) 0px 4px 12px'

export default function ListingCard({ listing }) {
  const imageUrl = listing.coverImageUrl || (listing.imageUrls?.[0])

  return (
    <div
      className="bg-white overflow-hidden cursor-pointer transition-shadow duration-200"
      style={{ borderRadius: '20px', boxShadow: CARD_SHADOW }}
      onMouseEnter={e => (e.currentTarget.style.boxShadow = HOVER_SHADOW)}
      onMouseLeave={e => (e.currentTarget.style.boxShadow = CARD_SHADOW)}
    >
      {/* Photo */}
      <div className="relative w-full" style={{ aspectRatio: '16 / 10' }}>
        {imageUrl ? (
          <img
            src={imageUrl}
            alt={listing.title}
            className="w-full h-full object-cover"
          />
        ) : (
          <div
            className="w-full h-full flex items-center justify-center"
            style={{ background: '#f2f2f2' }}
          >
            <svg width="40" height="40" viewBox="0 0 24 24" fill="none" stroke="#c1c1c1" strokeWidth="1.5">
              <rect x="3" y="3" width="18" height="18" rx="2" />
              <circle cx="8.5" cy="8.5" r="1.5" />
              <path d="M21 15l-5-5L5 21" />
            </svg>
          </div>
        )}
        {/* Wishlist button */}
        <button
          className="absolute top-3 right-3 flex items-center justify-center transition-colors"
          style={{
            width: '32px',
            height: '32px',
            borderRadius: '50%',
            background: 'rgba(255,255,255,0.85)',
            border: 'none',
          }}
          onMouseEnter={e => (e.currentTarget.style.background = '#ffffff')}
          onMouseLeave={e => (e.currentTarget.style.background = 'rgba(255,255,255,0.85)')}
          onClick={e => e.stopPropagation()}
          aria-label="Save to wishlist"
        >
          <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="#222222" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
            <path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z" />
          </svg>
        </button>
      </div>

      {/* Details */}
      <div className="p-4">
        <div className="flex justify-between items-start gap-3 mb-1">
          <h3
            className="font-semibold text-base line-clamp-1 flex-1"
            style={{ color: '#222222', letterSpacing: '-0.18px', lineHeight: '1.25' }}
          >
            {listing.title}
          </h3>
          <span
            className="font-semibold text-base whitespace-nowrap"
            style={{ color: '#222222' }}
          >
            ${listing.price}
          </span>
        </div>
        <p
          className="text-sm line-clamp-2"
          style={{ color: '#6a6a6a', lineHeight: '1.43' }}
        >
          {listing.description}
        </p>
        {listing.userName && (
          <p className="text-xs mt-2" style={{ color: '#6a6a6a' }}>
            {listing.userName}
          </p>
        )}
      </div>
    </div>
  )
}
