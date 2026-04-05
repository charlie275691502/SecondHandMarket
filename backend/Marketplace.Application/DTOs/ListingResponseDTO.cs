using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;

namespace Marketplace.Application.DTOs;

public class ListingResponseDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public double Latitude => Location.Y;
    public double Longitude => Location.X;
    public DateTime CreatedAt { get; set; }
    public string UserName { get; set; } = null!;

    [JsonIgnore]
    public Point Location { get; set; } = null!;
}