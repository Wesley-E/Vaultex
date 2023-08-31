using Vaultex.Models;

namespace Vaultex.Factories.Interfaces;

public interface IImportStrategyFactory
{
    public IImportStrategy CreateImport(Import import);
}