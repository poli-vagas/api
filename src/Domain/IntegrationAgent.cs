namespace PoliVagas.Core.Domain;

public class IntegrationAgent
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    #pragma warning disable CS8618 // Used by EF Migration
    private IntegrationAgent() {}
    # pragma warning restore CS8618

    private IntegrationAgent(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static IntegrationAgent Create(string name)
    {
        return new IntegrationAgent(Guid.NewGuid(), name);
    }
}
