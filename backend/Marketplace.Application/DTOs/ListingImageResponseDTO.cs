using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;

namespace Marketplace.Application.DTOs;

public class ListingImageResponseDTO
{
    public Guid Id { get; set; }
    public string Url { get; set; } = null!;
    public Guid ListingId { get; set; }
}