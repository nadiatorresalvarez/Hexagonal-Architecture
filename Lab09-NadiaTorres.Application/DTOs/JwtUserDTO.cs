namespace Lab09_NadiaTorres.Application.DTOs;

public class JwtUserDTO
{
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; // Para verificación de contraseña
    public string[] Roles { get; set; } = Array.Empty<string>();
}