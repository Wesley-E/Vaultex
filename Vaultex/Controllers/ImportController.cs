using Microsoft.AspNetCore.Mvc;
using Vaultex.Models;
using Vaultex.Services.Interfaces;
using Vaultex.ValueSets;

namespace Vaultex.Controllers;

[ApiController]
[Route("[controller]")]
public class ImportController : ControllerBase
{
    private readonly ILogger<ImportController> _logger;
    private readonly IImportService _importService;

    public ImportController(
        ILogger<ImportController> logger, 
        IImportService importService)
    {
        _logger = logger;
        _importService = importService;
    }

    [HttpPost(Name = "Import")]
    public IActionResult Import([FromBody] Import import)
    {
        try
        {
            _importService.Import(import);
            return Ok($"Successfully completed {Enum.ToObject(typeof(ImportType), import.ImportType)} import");
        }
        catch (Exception ex)
        {
            return BadRequest("Unable to import into database");
        }
        
    }
}