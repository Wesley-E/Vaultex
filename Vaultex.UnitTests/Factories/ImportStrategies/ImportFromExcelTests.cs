using Microsoft.Extensions.Logging;
using NSubstitute;
using Vaultex.Factories.ImportStrategies;
using Vaultex.Models;
using Vaultex.Repository;
using Xunit;

namespace Vaultex.UnitTests.Factories.ImportStrategies;

public class ImportFromExcelTests
{
    private readonly IPostgresRepository _repository;
    private readonly ILogger<ImportFromExcel> _logger;
    private readonly ImportFromExcel _sut;
    private const string FilePath = "../../../TestFiles/ExcelImportSample.xlsx";

    public ImportFromExcelTests()
    {
        _repository = Substitute.For<IPostgresRepository>();
        _logger = Substitute.For<ILogger<ImportFromExcel>>();
        _sut = new ImportFromExcel(_repository, _logger)
        {
            Path = FilePath
        };
    }

    [Fact]
    public void Import_ThrowsArgumentException_WhenPathIsNull()
    {
        var excelImport = new ImportFromExcel(_repository, _logger)
        {
            Path = "filePath"
        };
        Assert.Throws<ArgumentException>(() => excelImport.Import());
    }

    [Fact]
    public void Import_CallsRepositoryMethods_WhenPathIsNotNull()
    {
        _sut.Import();

        _repository.Received().ImportOrganisation(Arg.Any<IEnumerable<Organisation>>());
        _repository.Received().ImportEmployee(Arg.Any<IEnumerable<Employee>>());
    }
}