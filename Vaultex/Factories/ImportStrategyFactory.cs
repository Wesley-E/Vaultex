using Vaultex.Factories.ImportStrategies;
using Vaultex.Factories.Interfaces;
using Vaultex.Models;
using Vaultex.Repository;
using Vaultex.ValueSets;

namespace Vaultex.Factories;

public class ImportStrategyFactory : IImportStrategyFactory
{
    private readonly IPostgresRepository _repository;
    private readonly ILogger<ImportStrategyFactory> _logger;
    private readonly ILoggerFactory _loggerFactory;

    public ImportStrategyFactory(
        IPostgresRepository repository,
        ILogger<ImportStrategyFactory> logger,
        ILoggerFactory loggerFactory)
    {
        _repository = repository;
        _logger = logger;
        _loggerFactory = loggerFactory;
    }

    public IImportStrategy CreateImport(Import import)
    {
        return import.ImportType switch
        {
            ImportType.Excel => CreateExcelImport(import.fileName),
            _ => throw new ArgumentOutOfRangeException($"Unkown import type specified: {import.ImportType}")
        };
    }
    
    private IImportStrategy CreateExcelImport(string path)
    {
        _logger.LogInformation("Creating Excel Import strategy");
        var importFromExcel = new ImportFromExcel(_repository, path, _loggerFactory.CreateLogger<ImportFromExcel>());
        return importFromExcel;
    }
}