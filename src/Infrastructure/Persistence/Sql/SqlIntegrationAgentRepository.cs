using Microsoft.EntityFrameworkCore;
using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Infrastructure.Persistence;

public class SqlIntegrationAgentRepository : IIntegrationAgentRepository
{
    private SqlContext _dbContext;
    private DbSet<IntegrationAgent> _agents => _dbContext.Agents;

    public SqlIntegrationAgentRepository(SqlContext sqlContext)
    {
        _dbContext = sqlContext;
    }

    public async Task<IntegrationAgent> GetOrAdd(string agentName)
    {
        var agent = _agents.Where(c => c.Name == agentName).FirstOrDefault();

        if (agent == null) {
            agent = IntegrationAgent.Create(agentName);
            await Insert(agent);
        }

        return agent;
    }


    public async Task Insert(IntegrationAgent IntegrationAgent)
    {
        await _agents.AddAsync(IntegrationAgent);

        await _dbContext.SaveChangesAsync();
    }
}
