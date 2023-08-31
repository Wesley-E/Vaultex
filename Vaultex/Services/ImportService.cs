using Vaultex.Factories.Interfaces;
using Vaultex.Models;
using Vaultex.Services.Interfaces;

namespace Vaultex.Services;

public class ImportService : IImportService
{
    private readonly IImportStrategyFactory _importStrategyFactory;
    
    public ImportService(IImportStrategyFactory importStrategyFactory)
    {
        _importStrategyFactory = importStrategyFactory;
    }
    
    public void Import(Import import)
    {
        _importStrategyFactory.CreateImport(import).Import();
    }
}