using System.Diagnostics.CodeAnalysis;

namespace PoliVagas.Core.Domain;

public class Notification
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public JobFilter Filter { get; private set; }
    public DateTime LastRunTime { get; private set; }
    public DateTime CreatedTime { get; private set; }

    #pragma warning disable CS8618 // Used by EF Migration
    private Notification() {}
    #pragma warning restore CS8618

    private Notification(
        Guid id,
        string email,
        JobFilter filter,
        DateTime lastRunTime,
        DateTime createdTime
    ) {
        Id = id;
        Email = email;
        Filter = filter;
        LastRunTime = lastRunTime;
        CreatedTime = createdTime;
    }

    public static Notification Create(string email, JobFilter filter)
    {
        return new Notification(Guid.NewGuid(), email, filter, DateTime.MinValue, DateTime.UtcNow);
    }
}
