using Marketplace.Application.Interfaces;
using Marketplace.Core.Entities;
using Marketplace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
        var entry = await _context.Listings.AddAsync(listing);
        return entry.Entity;
    }

    async Task IListingRepository.SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}