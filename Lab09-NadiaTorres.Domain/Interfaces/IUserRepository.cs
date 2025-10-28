using Lab09_NadiaTorres.Application.DTOs;

namespace Lab09_NadiaTorres.Domain.Interfaces;

public interface IUserRepository
{
    Task<JwtUserDTO?> GetByUsernameAsync(string username);
}