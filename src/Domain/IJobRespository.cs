using PoliVagas.Core.Application.SearchJobs;

namespace PoliVagas.Core.Domain;

public interface IJobRepository
{
    /// <exception cref="JobNotFoundException">
    /// When the Job with the provided ID is not found.
    /// </exception>
    public Task<Job> FindById(Guid jobId);
    public bool TryFindByWeekNumber(int weekNumber, string description, out Job? job);
    public Task<IEnumerable<Job>> Find(Query query);
    public Task Insert(Job opportunity);
    public Task<int> Count(JobFilter filter);
}

public class JobNotFoundException : System.Exception
{
    public JobNotFoundException() {}
}
