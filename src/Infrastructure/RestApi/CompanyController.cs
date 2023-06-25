using Microsoft.AspNetCore.Mvc;
using PoliVagas.Core.Application.ListCompanies;
using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Infrastructure.RestApi;

[ApiController]
[Route("companies")]
[Produces("application/json")]
public class CompanyController : ControllerBase
{
    private readonly ILogger<CompanyController> _logger;
    private readonly ListCompaniesHandler _listCompaniesHandler;

    public CompanyController(
        ILogger<CompanyController> logger,
        ListCompaniesHandler listCompaniesHandler
    ) {
        _logger = logger;
        _listCompaniesHandler = listCompaniesHandler;
    }

    /// <summary>
    /// List all companies
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Company>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListAll()
    {
        _logger.LogInformation("Listing all companies...");

        var companies = await _listCompaniesHandler.Execute();
        return Ok(companies);
    }
}
