using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Vaultex.Controllers;
using Vaultex.Models;
using Vaultex.Services.Interfaces;
using Vaultex.ValueSets;
using Xunit;
using Assert = Xunit.Assert;

namespace Vaultex.UnitTests.Controllers;

public class ImportControllerTests
{
    private readonly ILogger<ImportController> _logger;
    private readonly IImportService _importService;
    private readonly ImportController _sut;

    public ImportControllerTests()
    {
        _logger = Substitute.For<ILogger<ImportController>>();
        _importService = Substitute.For<IImportService>();
        _sut = new ImportController(_logger, _importService);
    }

    [Fact]
    public void Import_ReturnsOkObjectResult_WhenNoExceptionOccurs()
    {
        var import = new Import { ImportType = (int)ImportType.Excel };
        
        var result = _sut.Import(import);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal($"Successfully completed {Enum.ToObject(typeof(ImportType), import.ImportType)} import", okResult.Value);
    }

    [Fact]
    public void Import_ReturnsBadRequestObjectResult_WhenExceptionOccurs()
    {
        var import = new Import { ImportType = (int)ImportType.Excel };
        _importService.When(i => i.Import(import)).Do(x => throw new Exception("Sample exception"));
        
        var result = _sut.Import(import);
        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Unable to import into database", badRequestResult.Value);
    }
}