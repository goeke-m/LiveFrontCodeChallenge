using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CartonCaps.Data.IoC;

/// <summary>
/// Provides extension methods for registering data dependencies in the dependency injection container.
/// </summary>
public static class DataDependencyInjection
{
    /// <summary>
    /// Registers all data dependencies in the dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the data dependencies.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the registered services.</returns>
    public static IServiceCollection AddDataDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ReferralDbContext>(options =>
        {
            // Ideally we would pull the configuration from a secure location, but for the sake of this challenge we will pull it from the appsettings.json file.
            var connectionString = configuration.GetSection("CONNECTION_STRING").Value;
            options.UseSqlServer(connectionString, x => x.MigrationsAssembly(Assembly.GetAssembly(typeof(DataDependencyInjection)).FullName));
        });

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var context = scope.ServiceProvider.GetService<ReferralDbContext>();
            context?.Database.EnsureCreated();
        }

        return services;
    }
}
