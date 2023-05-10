namespace PoliVagas.Core.Domain;

public class Course
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    #pragma warning disable CS8618 // Used by EF Migration
    private Course() {}
    # pragma warning restore CS8618

    private Course(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Course Create(string name)
    {
        return new Course(Guid.NewGuid(), name);
    }
}
