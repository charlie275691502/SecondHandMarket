using Marketplace.Application.DTOs;
using Marketplace.Core.Entities;

namespace Marketplace.Application.Interfaces;

public interface IListingImageRepository
{
    Task AddListingImageAsync(ListingImage listingImage);
    Task SaveChangesAsync();
}