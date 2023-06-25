using Microsoft.AspNetCore.Mvc;
using PoliVagas.Core.Application.ListCourses;
using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Infrastructure.RestApi;

[ApiController]
[Route("courses")]
[Produces("application/json")]
public class CourseController : ControllerBase
{
    private readonly ILogger<CourseController> _logger;
    private readonly ListCoursesHandler _listCoursesHandler;

    public CourseController(
        ILogger<CourseController> logger,
        ListCoursesHandler listCoursesHandler
    ) {
        _logger = logger;
        _listCoursesHandler = listCoursesHandler;
    }

    /// <summary>
    /// List all courses
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Course>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListAll()
    {
        _logger.LogInformation("Listing all courses...");

        var courses = await _listCoursesHandler.Execute();
        return Ok(courses);
    }
}
