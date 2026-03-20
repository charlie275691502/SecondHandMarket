using Marketplace.Application.Interfaces;
using Marketplace.Core.Entities;
using Marketplace.Infrastructure.Persistence;

namespace Marketplace.Infrastructure.Repositories;

public class ListingRepository : IListingRepository
{
    private readonly MarketplaceDbContext _context;

    public ListingRepository(MarketplaceDbContext context)
    {
        _context = context;
    }

    async Task IListingRepository.AddListingAsync(Listing listing)
    {
        await _context.Listings.AddAsync(listing);
    }

    async Task IListingRepository.SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}