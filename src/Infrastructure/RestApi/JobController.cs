using Microsoft.AspNetCore.Mvc;
using PoliVagas.Core.Application.CreateJob;
using PoliVagas.Core.Application.FindJob;
using PoliVagas.Core.Application.SearchJobs;
using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Infrastructure.RestApi;

[ApiController]
[Route("jobs")]
public class JobController : ControllerBase
{
    private readonly ILogger<JobController> _logger;
    private readonly CreateJobHandler _createHandler;
    private readonly FindJobHandler _findHandler;
    private readonly IJobRepository _jobs;

    public JobController(
        ILogger<JobController> logger,
        CreateJobHandler createHandler,
        FindJobHandler findHandler,
        IJobRepository jobs
    ) {
        _logger = logger;
        _createHandler = createHandler;
        _findHandler = findHandler;
        _jobs = jobs;
    }

    [HttpGet]
    [Route("{jobId}")]
    public async Task<IActionResult> Find(Guid jobId)
    {
        Job job;
        // try {
            job = await _findHandler.Execute(jobId);
        // } catch (System.Exception) {
        //     return NotFound();
        // }

        return Ok(job);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateJobCommand command)
    {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var job = await _createHandler.Execute(command);

        return Ok(job);
    }

    [HttpPost]
    [Route("search")]
    public async Task<IActionResult> Search([FromBody] Query query)
    {
        var result = await _jobs.Find(query);

        return Ok(result);
    }
}
