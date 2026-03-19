using Marketplace.Application.Interfaces;
using Marketplace.Core.Entities;
using Marketplace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MarketplaceDbContext _context;

    public UserRepository(MarketplaceDbContext context)
    {
        _context = context;
    }

    async Task<User?> IUserRepository.GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(user => user.Email == email);
    }

    async Task IUserRepository.AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    async Task IUserRepository.SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}