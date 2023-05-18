using PoliVagas.Core.Domain;
using PoliVagas.Core.Application.SearchJobs;

namespace PoliVagas.Core.Application.NotifyNewJobs;

public class NotifyNewJobsHandler
{
    private INotificationRepository _notifications;
    private IJobRepository _jobs;
    private IMailService _mailer;

    public NotifyNewJobsHandler(
        INotificationRepository notifications,
        IJobRepository jobs,
        IMailService mailer
    ) {
        _notifications = notifications;
        _jobs = jobs;
        _mailer = mailer;
    }

    public async Task Execute()
    {
        var notifications = await _notifications.GetAll();

        foreach (var notification in notifications) {
            var filter = notification.Filter;
            filter.MinCreatedTime = notification.LastRunTime;

            var query = new Query() { Filter = filter };
            var jobs = await _jobs.Find(query);

            if (jobs.Any()) {
                var message = $"<p>You have {jobs.Count()} new jobs.</p>";
                await _mailer.SendEmail("Test", notification.Email, message);
            }

            notification.LastRunTime = DateTime.UtcNow;
            await _notifications.Save(notification);
        }
    }
}
