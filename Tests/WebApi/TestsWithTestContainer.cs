using CartonCaps.Data;
using CartonCaps.Data.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Writers;
using Testcontainers.MsSql;

namespace CartonCaps.Tests.WebApi;

[SetUpFixture]
public class TestsWithTestContainer
{
    protected WebApplicationFactory<Program> Factory { get; set; }
    protected MsSqlContainer DbContainer { get; set; }
    public Fixture Fixture { get; set; }

    [OneTimeSetUp]
    public async Task Setup()
    {
        Fixture = new Fixture();
        DbContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .Build();
        await DbContainer.StartAsync();

        Environment.SetEnvironmentVariable("CONNECTION_STRING", DbContainer.GetConnectionString());
        Factory = new WebApplicationFactory<Program>();
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        await Factory.DisposeAsync();
        await DbContainer.StopAsync();
        await DbContainer.DisposeAsync();
    }

    protected IEnumerable<Referral> SeedReferrals(int quantity, string referralCode, ReferralStatus referralStatus = ReferralStatus.Pending)
    {
        var referrals = new List<Referral>();
        for (int i = 0; i < quantity; i++)
        {
            var referee = Fixture.Build<Referee>()
                .With(x => x.Birthday, new DateOnly(1984, 01, 12))
                .Without(x => x.Referral).Create();
            var referral = Fixture.Build<Referral>()
                .With(x => x.Referee, referee)
                .With(x => x.ReferralCode, referralCode)
                .With(x => x.ReferralStatus, referralStatus).Create();

            referrals.Add(referral);
        }

        using (var scope = Factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ReferralDbContext>();
            dbContext.Referrals.AddRange(referrals);
            dbContext.SaveChanges();
        }

        return referrals;
    }
}
