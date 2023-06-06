using PoliVagas.Core.Application.SearchJobs;
using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Infrastructure.Persistence;

public class InMemoryJobRepository : IJobRepository
{
    private List<Job> opportunities = new ();

    public Task<IEnumerable<Job>> Find(Query query)
    {
        throw new NotImplementedException();
    }

    public Task<Job> FindById(Guid jobId)
    {
        throw new NotImplementedException();
    }

    public Task Insert(Job opportunity)
    {
        opportunities.Add(opportunity);

        return Task.CompletedTask;
    }

    public Task<int> Count(JobFilter filter)
    {
        throw new NotImplementedException();
    }
}
