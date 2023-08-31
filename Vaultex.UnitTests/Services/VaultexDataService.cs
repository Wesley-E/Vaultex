using Microsoft.Extensions.Logging;
using NSubstitute;
using Vaultex.Models;
using Vaultex.Repository;
using Vaultex.Services;
using Xunit;

namespace Vaultex.UnitTests.Services;

public class VaultexDataServiceTests
{
    private readonly IPostgresRepository _repository;
    private readonly VaultexDataService _sut;

    public VaultexDataServiceTests()
    {
        _repository = Substitute.For<IPostgresRepository>();
        _sut = new VaultexDataService(_repository);
    }

    [Fact]
    public async Task GetAllEmployeesAsync_ReturnsEmployees()
    {
        var employees = new List<Employee> { new Employee { FirstName = "John", LastName = "Doe" } };
        _repository.GetAllEmployeesAsync().Returns(employees);

        var result = await _sut.GetAllEmployeesAsync();

        Assert.Equal(employees, result);
    }

    [Fact]
    public async Task GetAllOrganisationsAsync_ReturnsOrganisations()
    {
        var organisations = new List<Organisation> { new Organisation { OrganisationName = "Sample Organisation" } };
        _repository.GetAllOrganisationsAsync().Returns(organisations);

        var result = await _sut.GetAllOrganisationsAsync();

        Assert.Equal(organisations, result);
    }
}