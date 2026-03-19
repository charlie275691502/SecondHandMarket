using Marketplace.Core.Entities;

namespace Marketplace.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddUserAsync(User user);
    Task SaveChangesAsync();
}