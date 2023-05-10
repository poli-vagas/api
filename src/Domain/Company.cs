namespace PoliVagas.Core.Domain;

public class Company
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    #pragma warning disable CS8618 // Used by EF Migration
    private Company() {}
    #pragma warning restore CS8618

    private Company(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Company Create(string name)
    {
        return new Company(Guid.NewGuid(), name);
    }
}
