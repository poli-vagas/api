using PoliVagas.Core.Domain;
using PoliVagas.Core.Application.SearchJobs;
using System.Text;

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
                var sb = new StringBuilder();
                sb.Append($"<p>Novas vagas foram publicadas de acordo com sua busca:</p>");

                sb.Append("<ul>");
                foreach (var job in jobs) {
                    var type = job.Type switch {
                        JobType.Internship  => "Estágio",
                        JobType.Trainee     => "Trainee",
                        JobType.FullTime    => "Emprego",
                        _                   => job.Type.ToString(),
                    };
                    sb.Append($"<li><a href=\"https://poli-vagas.mario.engineering/dash/vacancies/{job.Id.ToString()}\">");
                    sb.Append($"{type} @ {job.Company.Name}");
                    sb.Append("</a></li>");
                }
                sb.Append("</ul>");

                var message = sb.ToString();
                await _mailer.SendEmail("PoliVagas - Alerta de novas vagas", notification.Email, message);
            }

            notification.LastRunTime = DateTime.UtcNow;
            await _notifications.Save(notification);
        }
    }
}
