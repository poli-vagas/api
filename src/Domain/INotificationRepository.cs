namespace PoliVagas.Core.Domain;

public interface INotificationRepository
{
    public Task<IEnumerable<Notification>> GetAll();
    public Task Save(Notification notification);
}
