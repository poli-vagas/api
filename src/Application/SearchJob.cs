using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Application.SearchJob;

public class Query
{
    public JobFilter Filter { get; set; } = new();
}

