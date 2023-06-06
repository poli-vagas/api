using Microsoft.AspNetCore.Mvc;
using PoliVagas.Core.Application.RegisterJob;
using PoliVagas.Core.Application.FindJob;
using PoliVagas.Core.Application.SearchJobs;
using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Infrastructure.RestApi;

[ApiController]
[Route("jobs")]
[Produces("application/json")]
public class JobController : ControllerBase
{
    private readonly ILogger<JobController> _logger;
    private readonly RegisterJobHandler _createHandler;
    private readonly FindJobHandler _findHandler;
    private readonly SearchJobsHandler _searchJobsHandler;

    public JobController(
        ILogger<JobController> logger,
        RegisterJobHandler createHandler,
        FindJobHandler findHandler,
        SearchJobsHandler searchJobsHandler
    ) {
        _logger = logger;
        _createHandler = createHandler;
        _findHandler = findHandler;
        _searchJobsHandler = searchJobsHandler;
    }

    /// <summary>
    /// Finds a specific job by its ID
    /// </summary>
    [HttpGet]
    [Route("{jobId}")]
    [ProducesResponseType(typeof(Job), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(Guid jobId)
    {
        Job job;
        try {
            job = await _findHandler.Execute(jobId);
        } catch (JobNotFoundException) {
            return NotFound();
        }

        return Ok(job);
    }

    /// <summary>
    /// Registers a new job
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Job), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] RegisterJobCommand command)
    {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var job = await _createHandler.Execute(command);

        return Created(nameof(Get), job);
    }

    /// <summary>
    /// Search jobs that match with a given filter
    /// </summary>
    [HttpPost]
    [Route("search")]
    [ProducesResponseType(typeof(SearchJobsResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromBody] Query query)
    {
        var result = await _searchJobsHandler.Execute(query);

        return Ok(result);
    }
}
