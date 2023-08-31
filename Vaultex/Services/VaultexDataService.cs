using Vaultex.Models;
using Vaultex.Repository;

namespace Vaultex.Services;

public interface IVaultexDataService
{
    Task<List<Employee>> GetAllEmployeesAsync();
    Task<List<Organisation>> GetAllOrganisationsAsync();
}

public class VaultexDataService : IVaultexDataService
{
    private readonly IPostgresRepository _repository;
    
    public VaultexDataService(
        IPostgresRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        return await _repository.GetAllEmployeesAsync();
    }
    
    public async Task<List<Organisation>> GetAllOrganisationsAsync()
    {
        return await _repository.GetAllOrganisationsAsync();
    }
}