using PoliVagas.Core.Application.SearchJob;

namespace PoliVagas.Core.Domain;

public interface IJobRepository
{
    public Task<IEnumerable<Job>> Find(Query query);
    public Task Insert(Job opportunity);
}
