using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Application.SearchJobs;

public class SearchJobsHandler
{
    private readonly IJobRepository _jobs;

    public SearchJobsHandler(IJobRepository jobs) {
        _jobs = jobs;
    }

    public async Task<SearchJobsResult> Execute(Query query) {
        var jobs = await _jobs.Find(query);
        var total = await _jobs.Count(new JobFilter());
        var totalFiltered = await _jobs.Count(query.Filter);

        return new SearchJobsResult(query.Page, query.PageSize, total, totalFiltered, jobs);
    }
}

public class Query
{
    public JobFilter Filter { get; set; } = new();
    public int PageSize { get; set; } = 10;
    public int Page { get; set; } = 0;
}

public class SearchJobsResult
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public int TotalFiltered { get; set; }
    public IEnumerable<Job> Jobs { get; set; }

    public SearchJobsResult(
        int page,
        int pageSize,
        int total,
        int totalFiltered,
        IEnumerable<Job> jobs
    ) {
        Page = page;
        PageSize = pageSize;
        Total = total;
        TotalFiltered = totalFiltered;
        Jobs = jobs;
    }
}