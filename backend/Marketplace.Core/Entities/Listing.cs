using NetTopologySuite.Geometries;
namespace Marketplace.Core.Entities;

public class Listing
{
    public enum ListingStatus
    {
        Draft,
        Published,
    }

    public Guid Id { get; set; }
    public ListingStatus Status { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public Point Location { get; set; } = null!;
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid CoverImageId { get; set; }
    public ListingImage CoverImage { get; set; } = null!;
    public List<ListingImage> Images { get; set; } = new();
}