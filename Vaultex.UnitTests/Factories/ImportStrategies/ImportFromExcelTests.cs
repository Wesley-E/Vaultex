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
        _sut = new ImportFromExcel(_repository, FilePath, _logger);
    }

    [Fact]
    public void Import_ThrowsArgumentException_WhenPathIsNull()
    {
        Assert.Throws<ArgumentException>(() => new ImportFromExcel(_repository, "filePath", _logger));
    }

    [Fact]
    public void Import_CallsRepositoryMethods_WhenPathIsNotNull()
    {
        _sut.Import();

        _repository.Received().ImportOrganisation(Arg.Any<IEnumerable<Organisation>>());
        _repository.Received().ImportEmployee(Arg.Any<IEnumerable<Employee>>());
    }
}