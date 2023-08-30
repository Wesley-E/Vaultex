using Vaultex.Models;

namespace Vaultex.Services;

public interface IImportService
{
    void Import(Import import);
}