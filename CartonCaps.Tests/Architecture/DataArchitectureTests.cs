using CartonCaps.Data.Entities;
using NetArchTest.Rules;

namespace CartonCaps.Tests.Architecture;

[TestFixture]
[Category(TestCategories.Architecture)]
[Parallelizable(ParallelScope.All)]
public class DataArchitectureTests : ArchitectureBaseTests
{
    [Test]
    public void Data_DoesNotHaveDependencyOnWeb()
    {
        var result = Types.InAssembly(Data).ShouldNot().HaveDependencyOn(WebNamespace).GetResult();

        PrintFailedResults(result, nameof(Data_DoesNotHaveDependencyOnWeb));
        result.IsSuccessful.ShouldBeTrue("Data project should not have a dependency on the web project");
    }

    [Test]
    public void Data_DoesNotHaveDependencyOnService()
    {
        var result = Types.InAssembly(Data).ShouldNot().HaveDependencyOn(ServiceNamespace).GetResult();

        PrintFailedResults(result, nameof(Data_DoesNotHaveDependencyOnService));
        result.IsSuccessful.ShouldBeTrue("Data project should not have a dependency on the service project");
    }

    [Test]
    public void DomainEntities_AreNotExposedToWeb()
    {
        var result = Types.InAssembly(Data).That().Inherit(typeof(BaseEntity)).ShouldNot().ResideInNamespace(WebNamespace).GetResult();

        PrintFailedResults(result, nameof(DomainEntities_AreNotExposedToWeb));
        result.IsSuccessful.ShouldBeTrue("Data entities should not be exposed to the web");
    }
}
