using Lab09_NadiaTorres.Application.DTOs;
using Lab09_NadiaTorres.Application.Interfaces;
using Lab09_NadiaTorres.Domain.Interfaces;

namespace Lab09_NadiaTorres.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtGenerator _jwt;

    public AuthService(IUserRepository userRepo, IPasswordHasher hasher, IJwtGenerator jwt)
    {
        _userRepo = userRepo;
        _hasher = hasher;
        _jwt = jwt;
    }

    public async Task<AuthResponseDTO?> AuthenticateAsync(LoginRequestDTO request)
    {
        // Obtener usuario y roles directamente desde el repositorio
        var user = await _userRepo.GetByUsernameAsync(request.Username);
        if (user == null || !_hasher.Verify(user.PasswordHash, request.Password))
            return null;

        // Generar token JWT
        var token = _jwt.GenerateToken(user);

        // Retornar respuesta con token y nombre de usuario
        return new AuthResponseDTO
        {
            Token = token,
            Username = user.Username
        };
    }
}