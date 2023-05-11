using PoliVagas.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace PoliVagas.Core.Application.Subscribe;

public class SubscribeHandler
{
    private INotificationRepository _notifications;

    public SubscribeHandler(INotificationRepository notifications)
    {
        _notifications = notifications;
    }

    public async Task<Notification> Execute(SubscribeCommand command)
    {
        var notification = Notification.Create(command.Email, command.Filter);

        await _notifications.Insert(notification);

        return notification;
    }
}

public class SubscribeCommand
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public JobFilter Filter { get; set; } = null!;
}
