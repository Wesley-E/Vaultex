using OfficeOpenXml;
using Vaultex.Factories.Interfaces;
using Vaultex.Models;
using Vaultex.Repository;

namespace Vaultex.Factories.ImportStrategies;

public class ImportFromExcel : IImportStrategy
{
    private string Path { get; set; }
    private readonly IPostgresRepository _repository;
    private readonly ILogger<ImportFromExcel> _logger;
    
    public ImportFromExcel(
        IPostgresRepository repository,
        string path,
        ILogger<ImportFromExcel> logger)
    {
        _repository = repository;
        _logger = logger;
        if (!File.Exists(path))
        {
            _logger.LogError("Path for excel import doesn't exist: {path}", path);
            throw new ArgumentException($"{path} doesn't exist");
        }
            
        Path = path;
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    public void Import()
    {
        var organisation = ExtractOrganisation(Path);
        var employee = ExtractEmployee(Path);
        _repository.ImportOrganisation(organisation);
        _repository.ImportEmployee(employee);
        _logger.LogInformation("Successfully imported data from: {path}", Path);
    }

    private IEnumerable<Organisation> ExtractOrganisation(string filePath)
    {
        _logger.LogInformation("Extracting organisations from: {path}", filePath);
        using var package = new ExcelPackage(new FileInfo(filePath));
        var result = new List<Organisation>();
        var worksheet = package.Workbook.Worksheets[0];
            
        var rowCount = worksheet.Dimension.Rows;
        for (var row = 2; row <= rowCount; row++) 
        {
            result.Add(new Organisation
            {
                OrganisationName = worksheet.Cells[row, 1].Value?.ToString(),
                OrganisationNumber = worksheet.Cells[row, 2].Value?.ToString(),
                AddressLine1 = worksheet.Cells[row, 3].Value?.ToString(),
                AddressLine2 = worksheet.Cells[row, 4].Value?.ToString(),
                AddressLine3 = worksheet.Cells[row, 5].Value?.ToString(),
                AddressLine4 = worksheet.Cells[row, 6].Value?.ToString(),
                Town = worksheet.Cells[row, 7].Value?.ToString(),
                Postcode = worksheet.Cells[row, 8].Value?.ToString(),
                Column9 = Convert.ToInt32(worksheet.Cells[row, 9].Value)
            });
        }
        return result;
    }
    
    private IEnumerable<Employee> ExtractEmployee(string filePath)
    {
        _logger.LogInformation("Extracting employees from: {path}", filePath);
        using var package = new ExcelPackage(new FileInfo(filePath));
        var result = new List<Employee>();
        var worksheet = package.Workbook.Worksheets[1];
            
        var rowCount = worksheet.Dimension.Rows;
        for (var row = 2; row <= rowCount; row++) 
        {
            result.Add(new Employee
            {
                OrganisationNumber = worksheet.Cells[row, 1].Value?.ToString(),
                FirstName = worksheet.Cells[row, 2].Value?.ToString(),
                LastName = worksheet.Cells[row, 3].Value?.ToString(),
            });
        }
        return result;
    }

}