using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vaultex.Services;

namespace Vaultex.Controllers;

[ApiController]
[Route("[controller]")]
public class OrganisationController
{
    private readonly ILogger<OrganisationController> _logger;
    private readonly IVaultexDataService _vaultexDataService;

    public OrganisationController(
        ILogger<OrganisationController> logger,
        IVaultexDataService vaultexDataService)
    {
        _logger = logger;
        _vaultexDataService = vaultexDataService;
    }
    
    [HttpGet(Name = "Get Organisations")]
    public async Task<IActionResult> GetOrganisations()
    {
        try
        {
            var organisationsAsync = await _vaultexDataService.GetAllOrganisationsAsync();
            return new OkObjectResult(organisationsAsync);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occured while retrieving organisations/n{ex}\n", ex);
            return new BadRequestObjectResult("Error occured. Unable to retrieve organisations.");
        }
    }
}