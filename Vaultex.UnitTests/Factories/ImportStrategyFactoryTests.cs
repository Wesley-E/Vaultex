using Microsoft.Extensions.Logging;
using NSubstitute;
using Vaultex.Factories;
using Vaultex.Factories.ImportStrategies;
using Vaultex.Models;
using Vaultex.Repository;
using Vaultex.ValueSets;
using Xunit;

namespace Vaultex.UnitTests.Factories;

public class ImportStrategyFactoryTests
{
    private readonly IPostgresRepository _repository;
    private readonly ILogger<ImportStrategyFactory> _logger;
    private readonly ILoggerFactory _loggerFactory;
    private readonly ImportStrategyFactory _sut;
    private const string FilePath = "../../../TestFiles/ExcelImportSample.xlsx";

    public ImportStrategyFactoryTests()
    {
        _repository = Substitute.For<IPostgresRepository>();
        _logger = Substitute.For<ILogger<ImportStrategyFactory>>();
        _loggerFactory = Substitute.For<ILoggerFactory>();
        _sut = new ImportStrategyFactory(_repository, _logger, _loggerFactory);
    }

    [Fact]
    public void CreateImport_ReturnsImportFromExcel_WhenImportTypeIsExcel()
    {
        var import = new Import { ImportType = ImportType.Excel, FileName = FilePath };

        var result = _sut.CreateImport(import);

        Assert.IsType<ImportFromExcel>(result);
    }

    [Fact]
    public void CreateImport_ThrowsArgumentOutOfRangeException_WhenImportTypeIsUnknown()
    {
        var import = new Import { ImportType = (ImportType)999, FileName = "sample.xlsx" };

        Assert.Throws<ArgumentOutOfRangeException>(() => _sut.CreateImport(import));
    }
}