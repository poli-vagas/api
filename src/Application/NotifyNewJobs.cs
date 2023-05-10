using PoliVagas.Core.Domain;
using PoliVagas.Core.Application.SearchJob;

namespace PoliVagas.Core.Application.NotifyNewJobs;

public class Handler
{
    private INotificationRepository _notifications;
    private IJobRepository _opportunities;
    private IMailService _mailer;

    public Handler(
        INotificationRepository notifications,
        IJobRepository opportunities,
        IMailService mailer
    ) {
        _notifications = notifications;
        _opportunities = opportunities;
        _mailer = mailer;
    }

    public async Task Execute()
    {
        var notifications = await _notifications.GetAll();

        foreach (var notification in notifications) {
            var query = new Query() { Filter = notification.Filter };
            var opportunities = await _opportunities.Find(query);

            await _mailer.SendEmail("Test", notification.Email, query.ToString() ?? "");
        }
    }
}
