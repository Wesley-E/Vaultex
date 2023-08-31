using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Vaultex.Controllers;
using Vaultex.Models;
using Vaultex.Services;
using Xunit;
using NSubstitute.ExceptionExtensions;
using Assert = Xunit.Assert;

namespace Vaultex.UnitTests.Controllers;

public class EmployeeControllerTests
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IVaultexDataService _vaultexDataService;
    private readonly EmployeeController _sut;

    public EmployeeControllerTests()
    {
        _logger = Substitute.For<ILogger<EmployeeController>>();
        _vaultexDataService = Substitute.For<IVaultexDataService>();
        _sut = new EmployeeController(_logger, _vaultexDataService);
    }

    [Fact]
    public async Task GetEmployees_ReturnsOkObjectResult_WhenNoExceptionOccurs()
    {
        _vaultexDataService.GetAllEmployeesAsync().Returns(new List<Employee>());
        
        var result = await _sut.GetEmployees();
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        var employees = Assert.IsAssignableFrom<IEnumerable<Employee>>(okResult.Value);
        Assert.Empty(employees);
    }

    [Fact]
    public async Task GetEmployees_ReturnsBadRequestObjectResult_WhenExceptionOccurs()
    {
        _vaultexDataService.GetAllEmployeesAsync().Throws(new Exception());
        
        var result = await _sut.GetEmployees();
        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Error occured. Unable to retrieve employees.", badRequestResult.Value);
    }
}
