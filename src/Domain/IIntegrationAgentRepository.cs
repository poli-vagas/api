namespace PoliVagas.Core.Domain;

public interface IIntegrationAgentRepository
{
    public Task<IntegrationAgent> GetOrAdd(string integrationAgentName);
    public Task Insert(IntegrationAgent integrationAgent);
}
