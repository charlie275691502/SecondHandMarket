using Marketplace.Core.Entities;

namespace Marketplace.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}