namespace PoliVagas.Core.Domain;

public class Job
{
    public Guid Id { get; private set; }
    public Company Company { get; private set; }
    public JobType Type { get; private set; }
    public int? Semester { get; private set; }
    public DateTime? LimitDate { get; private set; }
    public DateTime? GraduationDate { get; private set; }
    public IntegrationAgent? IntegrationAgent { get; private set; }
    public List<Course> Courses { get; private set; }
    public string Description { get; private set; }
    public string? Area { get; private set; }
    public Workplace? Workplace { get; private set; }
    // TODO: Modalidade (Presencial, Remoto, Hibrido)
    public int? HoursPerDay { get; private set; }
    public decimal? Salary { get; private set; }
    public Benefits Benefits { get; private set; }
    public Requirements Requirements { get; private set; }
    public Contact Contact { get; private set; }
    public DateTime CreatedTime { get; private set; }

    #pragma warning disable CS8618 // Used by EF Migration
    private Job() {}
    # pragma warning restore CS8618

    private Job(
        Guid id,
        Company company,
        JobType type,
        int? semester,
        DateTime? limitDate,
        DateTime? graduationDate,
        IntegrationAgent? integrationAgent,
        List<Course> courses,
        string description,
        string? area,
        Workplace? workplace,
        int? hoursPerDay,
        decimal? salary,
        Benefits benefits,
        Requirements requirements,
        Contact contact,
        DateTime createdTime
    ) {
        Id = id;
        Company = company;
        Type = type;
        Semester = semester;
        LimitDate = limitDate;
        GraduationDate = graduationDate;
        IntegrationAgent = integrationAgent;
        Courses = courses;
        Description = description;
        Area = area;
        Workplace = workplace;
        HoursPerDay = hoursPerDay;
        Salary = salary;
        Benefits = benefits;
        Requirements = requirements;
        Contact = contact;
        CreatedTime = createdTime;
    }

    public static Job Create(
        Company company,
        JobType type,
        int? semester,
        DateTime? limitDate,
        DateTime? graduationDate,
        IntegrationAgent? integrationAgent,
        List<Course> courses,
        string description,
        string? area,
        Workplace? workplace,
        int? hoursPerDay,
        decimal? salary,
        Benefits benefits,
        Requirements requirements,
        Contact contact
    ) {
        return new Job(
            Guid.NewGuid(),
            company,
            type,
            semester,
            limitDate,
            graduationDate,
            integrationAgent,
            courses,
            description,
            area,
            workplace,
            hoursPerDay,
            salary,
            benefits,
            requirements,
            contact,
            DateTime.UtcNow
        );
    }
}
