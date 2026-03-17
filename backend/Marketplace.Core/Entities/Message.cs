namespace Marketplace.Core.Entities;

public class Message
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public Guid ListingId { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}