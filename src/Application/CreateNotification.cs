using PoliVagas.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace PoliVagas.Core.Application.CreateNotification;

public class Handler
{
    private INotificationRepository _notifications;

    public Handler(INotificationRepository notifications)
    {
        _notifications = notifications;
    }

    public async Task<Notification> Execute(Command command)
    {
        var notification = Notification.Create(command.Email, command.Filter);

        await _notifications.Insert(notification);

        return notification;
    }
}

public class Command
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public JobFilter Filter { get; set; } = null!;
}
