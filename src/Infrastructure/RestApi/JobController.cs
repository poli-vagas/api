using Microsoft.AspNetCore.Mvc;
using PoliVagas.Core.Application.CreateJob;
using PoliVagas.Core.Application.SearchJob;
using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Infrastructure.RestApi;

[ApiController]
[Route("jobs")]
public class JobController : ControllerBase
{
    private readonly ILogger<JobController> _logger;
    private readonly CreateJobHandler _handler;
    private readonly IJobRepository _jobs;

    public JobController(
        ILogger<JobController> logger,
        CreateJobHandler handler,
        IJobRepository jobs
    ) {
        _logger = logger;
        _handler = handler;
        _jobs = jobs;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateJobCommand command)
    {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var opportunity = await _handler.Execute(command);

        return Ok(opportunity);
    }

    [HttpPost]
    [Route("search")]
    public async Task<IActionResult> Search([FromBody] Query query)
    {
        var result = await _jobs.Find(query);

        return Ok(result);
    }
}
