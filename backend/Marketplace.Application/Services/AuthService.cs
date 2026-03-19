using Marketplace.Application.DTOs;
using Marketplace.Application.Interfaces;
using Marketplace.Core.Entities;

namespace Marketplace.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    async Task<AuthResponseDTO> IAuthService.RegisterAsync(RegisterRequestDTO request)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new Exception("User already Exists");
        }

        var passwordHash = _passwordHasher.Hash(request.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            UserName = "New User",
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow,
        };

        await _userRepository.AddUserAsync(user);
        await _userRepository.SaveChangesAsync();

        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthResponseDTO
        {
            Token = token
        };
    }

    async Task<AuthResponseDTO> IAuthService.LoginAsync(LoginRequestDTO request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            throw new Exception("Email or Password Incorrect.");
        }

        var isVerified = _passwordHasher.Verify(request.Password, user.PasswordHash);
        if (!isVerified)
        {
            throw new Exception("Email or Password Incorrect.");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthResponseDTO
        {
            Token = token
        };
    }
}