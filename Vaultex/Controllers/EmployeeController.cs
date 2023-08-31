using Microsoft.AspNetCore.Mvc;
using Vaultex.Services;

namespace Vaultex.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IVaultexDataService _vaultexDataService;

    public EmployeeController(
        ILogger<EmployeeController> logger,
        IVaultexDataService vaultexDataService)
    {
        _logger = logger;
        _vaultexDataService = vaultexDataService;
    }
    
    [HttpGet(Name = "Get Employees")]
    public async Task<IActionResult> GetEmployees()
    {
        try
        {
            _logger.LogInformation("Request received: GetEmployees");
            var employeesAsync = await _vaultexDataService.GetAllEmployeesAsync();
            return new OkObjectResult(employeesAsync);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occured while retrieving employees/n{ex}\n", ex);
            return new BadRequestObjectResult("Error occured. Unable to retrieve employees.");
        }
    }
}