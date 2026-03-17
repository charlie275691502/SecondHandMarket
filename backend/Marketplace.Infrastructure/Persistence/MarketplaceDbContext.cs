using Microsoft.EntityFrameworkCore;
using Marketplace.Core.Entities;

namespace Marketplace.Infrastructure.Persistence;

public class MarketplaceDbContext : DbContext
{
    public MarketplaceDbContext(DbContextOptions<MarketplaceDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Listing> Listings => Set<Listing>();
    public DbSet<ListingImage> ListingImages => Set<ListingImage>();
    public DbSet<Message> Messages => Set<Message>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureUser(modelBuilder);
        ConfigureListing(modelBuilder);
        ConfigureListingImage(modelBuilder);
        ConfigureMessage(modelBuilder);
    }

    private static void ConfigureUser(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<User>();

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(200);

        entity.Property(x => x.PasswordHash)
            .IsRequired();

        entity.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(20);

        entity.Property(x => x.CreatedAt)
            .IsRequired();

        entity.HasMany(x => x.Listings)
            .WithOne(y => y.User)
            .HasForeignKey(y => y.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureListing(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Listing>();

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        entity.Property(x => x.Description)
            .IsRequired();

        entity.Property(x => x.Price)
            .HasColumnType("decimal(10,2)");

        entity.Property(x => x.CreatedAt)
            .IsRequired();

        entity.Property(x => x.Location)
            .HasColumnType("geography(Point, 4326)");

        entity.HasMany(x => x.Images)
            .WithOne(y => y.Listing)
            .HasForeignKey(y => y.ListingId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureListingImage(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<ListingImage>();

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Url)
            .IsRequired();
    }

    private static void ConfigureMessage(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Message>();

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(2000);

        entity.Property(x => x.CreatedAt)
            .IsRequired();
    }
}