namespace Marketplace.Application.DTOs;

public class CreateListingResponseDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserName { get; set; } = null!;
}