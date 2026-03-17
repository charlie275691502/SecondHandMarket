namespace Marketplace.Core.Entities;

public class ListingImage
{
    public Guid Id { get; set; }
    public string Url { get; set; } = null!;
    public Guid ListingId { get; set; }
    public Listing Listing { get; set; } = null!;
}