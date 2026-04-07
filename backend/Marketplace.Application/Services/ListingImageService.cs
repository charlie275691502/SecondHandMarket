using Marketplace.Application.DTOs;
using Marketplace.Application.Exceptions;
using Marketplace.Application.Interfaces;
using Marketplace.Core.Entities;
using Microsoft.AspNetCore.Http;
using NetTopologySuite.Geometries;

namespace Marketplace.Application.Services;

public class ListingImageService : IListingImageService
{
    private readonly IListingImageRepository _listingImageRepository;
    private readonly IImageStorage _imageStorage;

    public ListingImageService(
        IListingImageRepository listingImageRepository,
        IImageStorage imageStorage)
    {
        _listingImageRepository = listingImageRepository;
        _imageStorage = imageStorage;
    }

    async Task<ListingImageResponseDTO> IListingImageService.CreateListingImageAsync(Guid listingId, IFormFile file)
    {
        var imageUrl = await _imageStorage.UploadImageAsync(file);
        var listingImage = new ListingImage
        {
            Id = Guid.NewGuid(),
            Url = imageUrl,
            ListingId = listingId
        };

        await _listingImageRepository.AddListingImageAsync(listingImage);
        await _listingImageRepository.SaveChangesAsync();

        return new ListingImageResponseDTO
        {
            Id = listingImage.Id,
            Url = imageUrl,
            ListingId = listingId,
        };
    }
}