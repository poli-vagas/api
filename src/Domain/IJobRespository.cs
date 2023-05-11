using PoliVagas.Core.Application.SearchJobs;

namespace PoliVagas.Core.Domain;

public interface IJobRepository
{
    public Task<Job> FindById(Guid jobId);
    public Task<IEnumerable<Job>> Find(Query query);
    public Task Insert(Job opportunity);
}
