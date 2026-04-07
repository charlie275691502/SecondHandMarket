using Marketplace.Application.DTOs;
using Marketplace.Application.Interfaces;
using Marketplace.Core.Entities;
using Marketplace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Marketplace.Infrastructure.Repositories;

public class ListingImageRepository : IListingImageRepository
{
    private readonly MarketplaceDbContext _context;

    public ListingImageRepository(MarketplaceDbContext context)
    {
        _context = context;
    }

    async Task IListingImageRepository.AddListingImageAsync(ListingImage listingImage)
    {
        await _context.ListingImages.AddAsync(listingImage);
    }

    async Task IListingImageRepository.SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}