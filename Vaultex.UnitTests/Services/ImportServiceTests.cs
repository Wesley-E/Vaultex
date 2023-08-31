using NSubstitute;
using Vaultex.Factories.Interfaces;
using Vaultex.Models;
using Vaultex.Services;
using Vaultex.ValueSets;
using Xunit;

namespace Vaultex.UnitTests.Services;

public class ImportServiceTests
{
    private readonly IImportStrategyFactory _importStrategyFactory;
    private readonly ImportService _sut;

    public ImportServiceTests()
    {
        _importStrategyFactory = Substitute.For<IImportStrategyFactory>();
        _sut = new ImportService(_importStrategyFactory);
    }

    [Fact]
    public void Import_CallsCreateImportAndImportMethods()
    {
        var import = new Import { ImportType = ImportType.Excel, FileName = "sample.xlsx" };
        var importStrategy = Substitute.For<IImportStrategy>();
        _importStrategyFactory.CreateImport(import).Returns(importStrategy);

        _sut.Import(import);

        _importStrategyFactory.Received().CreateImport(import);
        importStrategy.Received().Import();
    }
}
