export default function ListingCard({ listing }) {
  return (
    <div className="bg-white border border-gray-200 rounded-xl p-5 hover:shadow-md transition-shadow">
      <div className="flex justify-between items-start mb-2">
        <h3 className="font-semibold text-gray-900 text-lg">{listing.title}</h3>
        <span className="text-indigo-600 font-bold text-lg">
          ${listing.price}
        </span>
      </div>
      <p className="text-gray-500 text-sm line-clamp-3">{listing.description}</p>
    </div>
  )
}
