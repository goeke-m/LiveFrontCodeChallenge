using CartonCaps.Data;
using Microsoft.EntityFrameworkCore;

namespace CartonCaps.Tests.Services;

public abstract class TestsWithInMemoryDb
{
    protected TestsWithInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<ReferralDbContext>()
            .UseInMemoryDatabase(databaseName: "CartonCaps")
            .Options;
        ReferralDbContext = new ReferralDbContext(options);

        if (Fixture == null)
        {
            Fixture = new Fixture();
            Fixture.Behaviors.Clear();
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
    protected ReferralDbContext ReferralDbContext { get; set; }
    public static Fixture Fixture { get; private set; } = null!;
}
