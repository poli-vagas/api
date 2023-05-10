using Microsoft.EntityFrameworkCore;
using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Infrastructure.Persistence;

public class SqlNotificationRepository : INotificationRepository
{
    private SqlContext _dbContext;
    private DbSet<Notification> _notifications => _dbContext.Notifications;

    public SqlNotificationRepository(SqlContext sqlContext)
    {
        _dbContext = sqlContext;
    }

    public async Task Insert(Notification Notification)
    {
        await _notifications.AddAsync(Notification);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Notification>> GetAll()
    {
        return await _notifications.ToListAsync();
    }
}
