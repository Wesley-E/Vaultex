using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Vaultex.Controllers;
using Vaultex.Models;
using Vaultex.Services;
using Xunit;

namespace Vaultex.UnitTests.Controllers;

public class OrganisationControllerTests
{
    private readonly ILogger<OrganisationController> _logger;
    private readonly IVaultexDataService _vaultexDataService;
    private readonly OrganisationController _sut;

    public OrganisationControllerTests()
    {
        _logger = Substitute.For<ILogger<OrganisationController>>();
        _vaultexDataService = Substitute.For<IVaultexDataService>();
        _sut = new OrganisationController(_logger, _vaultexDataService);
    }

    [Fact]
    public async Task GetOrganisations_ReturnsOkObjectResult_WhenNoExceptionOccurs()
    {
        _vaultexDataService.GetAllOrganisationsAsync().Returns(new List<Organisation>());
        
        var result = await _sut.GetOrganisations();
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        var organisations = Assert.IsAssignableFrom<IEnumerable<Organisation>>(okResult.Value);
        Assert.Empty(organisations);
    }

    [Fact]
    public async Task GetOrganisations_ReturnsBadRequestObjectResult_WhenExceptionOccurs()
    {
        _vaultexDataService.GetAllOrganisationsAsync().Throws(new Exception());
        
        var result = await _sut.GetOrganisations();
        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Error occured. Unable to retrieve organisations.", badRequestResult.Value);
    }
}