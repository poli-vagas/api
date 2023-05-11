using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Application.SearchJobs;

public class Query
{
    public JobFilter Filter { get; set; } = new();
}

