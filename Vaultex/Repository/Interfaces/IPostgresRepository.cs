using Vaultex.Models;

namespace Vaultex.Repository;

public interface IPostgresRepository
{
    void ImportOrganisation(IEnumerable<Organisation> organisation);
    void ImportEmployee(IEnumerable<Employee> employee);
    Task<List<Employee>> GetAllEmployeesAsync();
    Task<List<Organisation>> GetAllOrganisationsAsync();
}