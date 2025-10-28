using Lab09_NadiaTorres.Domain.Interfaces;
using Lab09_NadiaTorres.Infrastructure.Adapters;
using Lab09_NadiaTorres.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lab09_NadiaTorres.Infrastructure.Configuration;

public static class InfrastructureServicesExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuración de la conexión a la base de datos
        services.AddDbContext<dbContextnLab10>((serviceProvider, options) =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        // Registro de repositorios
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}