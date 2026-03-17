namespace Marketplace.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public List<Listing> Listings { get; set; } = new();
}