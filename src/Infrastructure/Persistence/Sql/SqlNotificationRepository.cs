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

    public async Task Save(Notification notification)
    {
        if (_notifications.Entry(notification).State == EntityState.Detached) {
            await _notifications.AddAsync(notification);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Notification>> GetAll()
    {
        return await _notifications.ToListAsync();
    }
}
