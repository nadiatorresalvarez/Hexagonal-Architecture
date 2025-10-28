using Lab09_NadiaTorres.Application.DTOs;

namespace Lab09_NadiaTorres.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDTO?> AuthenticateAsync(LoginRequestDTO request);
}