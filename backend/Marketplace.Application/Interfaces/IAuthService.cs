using Marketplace.Application.DTOs;

namespace Marketplace.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDTO> RegisterAsync(RegisterRequestDTO request);
    Task<AuthResponseDTO> LoginAsync(LoginRequestDTO request);
}