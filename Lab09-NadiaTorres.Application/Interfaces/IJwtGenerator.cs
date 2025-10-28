namespace Lab09_NadiaTorres.Application.Interfaces;

public interface IJwtGenerator
{
    string GenerateToken(Lab09_NadiaTorres.Application.DTOs.JwtUserDTO user);
}