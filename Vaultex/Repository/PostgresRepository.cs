using Microsoft.EntityFrameworkCore;
using Npgsql;
using Vaultex.Models;

namespace Vaultex.Repository;

public class PostgresRepository : IPostgresRepository
{
    private readonly ILogger<PostgresRepository> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public PostgresRepository(
        ILogger<PostgresRepository> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        var connectionString = _configuration.GetConnectionString("PostgresConnection") ??
                               throw new InvalidOperationException("PostgresConnection not provided in appsettings");
        _context = new ApplicationDbContext(connectionString);
    }

    public void ImportOrganisation(IEnumerable<Organisation> organisation)
    {
        _logger.LogInformation("Importing Organisations");
        const string tableName = "Organisations";
        _context.Database.EnsureCreated(); // Create the database if it doesn't exist

        var existingTable = _context.Model.GetEntityTypes().FirstOrDefault(t => t.GetTableName() == tableName);
        if (existingTable == null)
        {
            _logger.LogDebug("Organisation Table doesn't exist... Creating table.");
            const string createImportOrganisationEmployeeTable = $"CREATE TABLE {tableName} (" +
                                                                 $"\"OrganisationName\" VARCHAR(255), " +
                                                                 $"\"OrganisationNumber\" VARCHAR(255), " +
                                                                 $"\"AddressLine1\" VARCHAR(255), " +
                                                                 $"\"AddressLine2\" VARCHAR(255), " +
                                                                 $"\"AddressLine3\" VARCHAR(255), " +
                                                                 $"\"AddressLine4\" VARCHAR(255), " +
                                                                 $"\"Town\" VARCHAR(255), " +
                                                                 $"\"Postcode\" VARCHAR(255), " +
                                                                 $"\"Column9\" INTEGER)";
            ExecuteSql(createImportOrganisationEmployeeTable);
        }

        _context.Organisations.AddRange(organisation);
        _context.SaveChanges();
        _logger.LogInformation("Completed Importing Organisations");
    }

    public void ImportEmployee(IEnumerable<Employee> employee)
    {
        _logger.LogInformation("Importing Employees");
        const string tableName = "Employees";
        _context.Database.EnsureCreated(); // Create the database if it doesn't exist

        var existingTable = _context.Model.GetEntityTypes().FirstOrDefault(t => t.GetTableName() == tableName);
        if (existingTable == null)
        {
            _logger.LogDebug("Employee Table doesn't exist... Creating table.");
            const string createImportEmployeeTable = $"CREATE TABLE {tableName} (" +
                                                     $"\"OrganisationNumber\" VARCHAR(255), " + 
                                                     $"\"FirstName\" VARCHAR(255), " + 
                                                     $"\"LastName\" VARCHAR(255)";
            ExecuteSql(createImportEmployeeTable);
        }

        _context.Employees.AddRange(employee);
        _context.SaveChanges();
        _logger.LogInformation("Completed Importing Employees");
    }

    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        const string tableName = "Employees";
        var existingTable = _context.Model.GetEntityTypes().FirstOrDefault(t => t.GetTableName() == tableName);
        
        if (existingTable != null) return _context.Employees.ToList();
        
        _logger.LogError($"{tableName} table doesn't exist");
        return new List<Employee>();

    }

    public async Task<List<Organisation>> GetAllOrganisationsAsync()
    {
        const string tableName = "Employees";
        var existingTable = _context.Model.GetEntityTypes().FirstOrDefault(t => t.GetTableName() == tableName);
        
        if (existingTable != null) return _context.Organisations.ToList();
        
        _logger.LogError($"{tableName} table doesn't exist");
        return new List<Organisation>();

    }

    private void ExecuteSql(string sql)
    {
        _context.Database.ExecuteSqlRaw(sql);
    }
}