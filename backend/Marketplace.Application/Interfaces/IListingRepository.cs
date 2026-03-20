using Marketplace.Core.Entities;

namespace Marketplace.Application.Interfaces;

public interface IListingRepository
{
    Task<Listing> AddListingAsync(Listing listing);
    Task SaveChangesAsync();
}