using System.Text.Json.Serialization;
using Marketplace.Core.Entities;
using NetTopologySuite.Geometries;
using static Marketplace.Core.Entities.Listing;

namespace Marketplace.Application.DTOs;

public class ListingResponseDTO
{
    public Guid Id { get; set; }
    public ListingStatus Status { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public double Latitude => Location.Y;
    public double Longitude => Location.X;
    public DateTime CreatedAt { get; set; }
    public string UserName { get; set; } = null!;
    public string CoverImageUrl { get; set; } = null!;
    public List<string> ImageUrls { get; set; } = new();

    [JsonIgnore]
    public Point Location { get; set; } = null!;

    public ListingResponseDTO(Listing listing)
    {
        Id = listing.Id;
        Status = listing.Status;
        Title = listing.Title;
        Description = listing.Description;
        Price = listing.Price;
        Location = listing.Location;
        CreatedAt = listing.CreatedAt;
        UserName = listing.User.UserName;
        CoverImageUrl = listing.CoverImage?.Url ?? string.Empty;
        ImageUrls = listing.Images.Select(listingImage => listingImage.Url).ToList();
    }
}