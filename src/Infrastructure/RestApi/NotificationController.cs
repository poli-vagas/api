using Microsoft.AspNetCore.Mvc;
using PoliVagas.Core.Application.CreateNotification;

namespace PoliVagas.Core.Infrastructure.RestApi;

[ApiController]
[Route("notifications")]
public class NotificationController : ControllerBase
{
    private readonly ILogger<NotificationController> _logger;
    private readonly Handler _handler;
    private readonly PoliVagas.Core.Application.NotifyNewJobs.Handler _notify;

    public NotificationController(
        ILogger<NotificationController> logger,
        Handler handler,
        PoliVagas.Core.Application.NotifyNewJobs.Handler notify
    ) {
        _logger = logger;
        _handler = handler;
        _notify = notify;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Command command)
    {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var notification = await _handler.Execute(command);

        return Ok(notification);
    }

    // [HttpPost]
    // [Route("notify")]
    // public async Task<IActionResult> Notify()
    // {
    //     await _notify.Execute();

    //     return Ok();
    // }
}
