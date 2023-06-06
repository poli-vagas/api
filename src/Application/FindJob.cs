using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Application.FindJob;

public class FindJobHandler
{
    private readonly IJobRepository _jobs;

    public FindJobHandler(IJobRepository jobs) {
        _jobs = jobs;
    }

    public async Task<Job> Execute(Guid jobId) {
        return  await _jobs.FindById(jobId);
    }
}
