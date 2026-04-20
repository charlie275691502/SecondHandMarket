using Marketplace.Application.DTOs;
using Marketplace.Application.Exceptions;
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

    async Task<Listing> IListingRepository.AddEmptyListingAsync(Guid userId)
    {
        var emptyListing = new Listing
        {
            Id = Guid.NewGuid(),
            Status = Listing.ListingStatus.Draft,
            Title = "",
            Description = "",
            Price = 0,
            CreatedAt = DateTime.UtcNow,
            Location = new Point(0, 0),
            UserId = userId
        };
        await _context.Listings.AddAsync(emptyListing);
        await _context.SaveChangesAsync();

        return await _context.Listings
            .Include(l => l.User)
            .Include(l => l.Images)
            .FirstAsync(l => l.Id == emptyListing.Id);
    }

    async Task<Listing> IListingRepository.UpdateListingAsync(
        Guid listingId,
        string title,
        string description,
        decimal price,
        Point location,
        string coverImageUrl)
    {
        var listing = await _context.Listings
            .Include(l => l.User)
            .Include(l => l.Images)
            .FirstAsync(l => l.Id == listingId);

        var listingImage = await _context.ListingImages
            .FirstAsync(l => l.Url == coverImageUrl);

        if (listing == null)
        {
            throw new BadRequestException("Listing not found");
        }

        if (listingImage == null)
        {
            throw new BadRequestException("Image url not found");
        }

        listing.Title = title;
        listing.Description = description;
        listing.Price = price;
        listing.Location = location;
        listing.CoverImageId = listingImage.Id;

        await _context.SaveChangesAsync();
        return listing;
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
            .Where(listing => listing.Status == Listing.ListingStatus.Published)
            .Include(l => l.User)
            .Include(l => l.Images)
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

    async Task<Listing?> IListingRepository.GetListingByIdAsync(Guid listingId)
    {
        return await _context.Listings
            .Include(l => l.User)
            .Include(l => l.Images)
            .FirstAsync(l => l.Id == listingId);
    }

    async Task IListingRepository.SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}