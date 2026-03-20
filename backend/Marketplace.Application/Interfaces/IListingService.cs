using Marketplace.Application.DTOs;

namespace Marketplace.Application.Interfaces;

public interface IListingService
{
    Task<CreateListingResponseDTO> CreateListingAsync(Guid userId, CreateListingRequestDTO request);
}