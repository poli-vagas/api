using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Infrastructure.Persistence;

public class InMemoryIntegrationAgentRepository : IIntegrationAgentRepository
{
    private List<IntegrationAgent> agents = new ();

    public Task<IntegrationAgent> GetOrAdd(string agentName)
    {
        var agent = agents.Where(a => a.Name == agentName).FirstOrDefault();

        if (agent == null) {
            agent = IntegrationAgent.Create(agentName);
            agents.Add(agent);
        }

        return Task.FromResult(agent);
    }

    public Task Insert(IntegrationAgent agent)
    {
        agents.Add(agent);

        return Task.CompletedTask;
    }
}
