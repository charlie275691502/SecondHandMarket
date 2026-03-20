namespace Marketplace.Application.DTOs;

public class CreateListingRequestDTO
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}