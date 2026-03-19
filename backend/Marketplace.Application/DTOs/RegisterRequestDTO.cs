namespace Marketplace.Application.DTOs;

public class RegisterRequestDTO
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}