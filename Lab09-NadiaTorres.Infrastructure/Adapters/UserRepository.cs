using Lab09_NadiaTorres.Domain.Interfaces;
using Lab09_NadiaTorres.Application.DTOs;
using Lab09_NadiaTorres.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab09_NadiaTorres.Infrastructure.Adapters;

public class UserRepository : IUserRepository
{
    private readonly dbContextnLab10 _context;

    public UserRepository(dbContextnLab10 context)
    {
        _context = context;
    }

    public async Task<JwtUserDTO?> GetByUsernameAsync(string username)
    {
        // Cargar usuario y roles directamente
        var user = await _context.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null) return null;

        // Mapear a JwtUserDTO
        return new JwtUserDTO
        {
            Id = user.UserId.ToString(),
            Username = user.Username,
            PasswordHash = user.PasswordHash, // Agregar hash para verificación
            Roles = user.UserRoles.Select(ur => ur.Role.RoleName).ToArray()
        };
    }
}