using Vaultex.ValueSets;

namespace Vaultex.Models;

public class Import
{
    public ImportType ImportType { get; set; }
    public string? FileName { get; set; }
}