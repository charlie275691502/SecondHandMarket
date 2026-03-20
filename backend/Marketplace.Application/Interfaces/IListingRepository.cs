using Marketplace.Core.Entities;

namespace Marketplace.Application.Interfaces;

public interface IListingRepository
{
    Task AddListingAsync(Listing listing);
    Task SaveChangesAsync();
}