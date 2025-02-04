using CartonCaps.Data.Entities;
using CartonCaps.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
            var connectionString = configuration.GetSection("CONNECTION_STRING").Value;
            options.UseSqlServer(connectionString, x => x.MigrationsAssembly(Assembly.GetAssembly(typeof(DataDependencyInjection)).FullName))
            .UseSeeding((context, _) =>
            {
                context.Database.EnsureCreated();
                SeedData(context);
            });
        });
        
        return services;
    }

    private static void SeedData(DbContext context)
    {
        var referralCode = "X5YGP01";
        var willieReferralId = Guid.Parse("24278723-2248-48DA-A6F2-C7BA4056A144");
        var marshaReferralId = Guid.Parse("143C90ED-83C2-4CA7-9C07-24957CFADDDF");
        var geneReferralId = Guid.Parse("D69171D3-5A79-464B-9AC3-6DC220C07E30");

       var willieReferral = context.Set<Referral>().FirstOrDefault(r => willieReferralId == r.Id);

        if (willieReferral is null)
        {
            context.Add(new Referral
            {
                Id = willieReferralId,
                ReferralCode = referralCode,
                Referee = new Referee
                {
                    FirstName = "Willie",
                    LastName = "Makeit",
                    Email = "williemakeit@gmail.com"
                },
                ReferralStatus = ReferralStatus.Complete,
            });

            context.SaveChanges();
        }

        var marshaReferral = context.Set<Referral>().FirstOrDefault(r => marshaReferralId == r.Id);

        if (marshaReferral is null)
        {
            context.Add(new Referral
            {
                Id = marshaReferralId,
                ReferralCode = referralCode,
                Referee = new Referee
                {
                    FirstName = "Marsha",
                    LastName = "Mellow",
                    PhoneNumber = "555-908-4836"
                },
                ReferralStatus = ReferralStatus.Complete,
            });

            context.SaveChanges();
        }

        var geneReferral = context.Set<Referral>().FirstOrDefault(r => geneReferralId == r.Id);

        if (geneReferral is null)
        {
            context.Add(new Referral
            {
                Id = geneReferralId,
                ReferralCode = referralCode,
                Referee = new Referee
                {
                    FirstName = "Gene",
                    LastName = "Pool",
                    Email = "genepool@gmail.com"
                },
                ReferralStatus = ReferralStatus.Pending,
            });

            context.SaveChanges();
        }
    }
}
