using Marketplace.Application.Interfaces;

namespace Marketplace.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    string IPasswordHasher.Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    bool IPasswordHasher.Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}