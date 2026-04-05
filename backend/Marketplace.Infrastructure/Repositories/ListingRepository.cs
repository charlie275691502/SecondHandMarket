using Marketplace.Application.Interfaces;
using Marketplace.Core.Entities;
using Marketplace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Marketplace.Infrastructure.Repositories;

public class ListingRepository : IListingRepository
{
    private readonly MarketplaceDbContext _context;

    public ListingRepository(MarketplaceDbContext context)
    {
        _context = context;
    }

    async Task<Listing> IListingRepository.AddListingAsync(Listing listing)
    {
        await _context.Listings.AddAsync(listing);
        await _context.SaveChangesAsync();

        return await _context.Listings
            .Include(l => l.User)
            .FirstAsync(l => l.Id == listing.Id);
    }

    async Task<List<Listing>> IListingRepository.GetListingsAsync(
        string? keyword = null,
        double? latitude = null,
        double? longitude = null,
        double? radiusKm = null,
        int skip = 0,
        int take = 20)
    {
        var query = _context.Listings
            .Include(l => l.User)
            .AsNoTracking();
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(listing => listing.Title.Contains(keyword) || listing.Description.Contains(keyword));
        }

        if (latitude.HasValue && longitude.HasValue && radiusKm.HasValue)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var userLocation = geometryFactory.CreatePoint(new Coordinate(longitude.Value, latitude.Value));

            query = query.Where(listing => listing.Location.IsWithinDistance(userLocation, radiusKm.Value * 1000));
        }

        query = query
            .OrderByDescending(listing => listing.CreatedAt)
            .Skip(skip)
            .Take(take);

        return await query.ToListAsync();
    }

    async Task IListingRepository.SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}