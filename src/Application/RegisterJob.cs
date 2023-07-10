using System.ComponentModel.DataAnnotations;
using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Application.RegisterJob;

public class RegisterJobHandler
{
    private ICompanyRepository _companies;
    private IIntegrationAgentRepository _agents;
    private ICourseRepository _courses;
    private IJobRepository _jobs;

    public RegisterJobHandler(
        ICompanyRepository companies,
        IIntegrationAgentRepository agents,
        ICourseRepository courses,
        IJobRepository jobs
    ) {
        _companies = companies;
        _agents = agents;
        _courses = courses;
        _jobs = jobs;
    }

    public async Task<Job> Execute(RegisterJobCommand command)
    {
        if (command.WeekNumber != null) {
            var found = _jobs.TryFindByWeekNumber(
                command.WeekNumber ?? 0,
                command.Description,
                out var job
            );

            if (found && job != null) return job;
        }

        var benefits = new Benefits() {
            HasFoodVoucher = command.HasFoodVoucher,
            HasTransportVoucher = command.HasTransportVoucher,
            HasHealthInsurance = command.HasHealthInsurance,
            HasLifeInsurance = command.HasLifeInsurance,
        };

        var requirements = new Requirements() {
            EnglishLevel = command.EnglishLevel,
            OtherLanguages = command.OtherLanguages,
            SoftSkills = command.SoftSkills,
            HardSkills = command.HardSkills,
        };

        var contact = new Contact() {
            LinkedinUrl = command.LinkedinUrl,
            Email = command.Email,
            EmailInstructions = command.EmailInstructions,
            Phone = command.Phone,
            Url = command.Url,
            ExternalId = command.ExternalId,
        };

        var company = await _companies.GetOrAdd(command.CompanyName);

        IntegrationAgent? agent = null;
        if (command.IntegrationAgentName != null) {
            agent = await _agents.GetOrAdd(command.IntegrationAgentName);
        }

        var courses = new List<Course>();
        foreach (var courseName in command.Courses) {
            courses.Add(_courses.GetOrAdd(courseName).Result);
        }

        var opportunity = Job.Create(
            company,
            command.Type,
            command.Semester,
            command.LimitDate,
            command.GraduationDate,
            agent,
            courses,
            command.Description,
            command.Area,
            command.Workplace,
            command.HoursPerDay,
            command.Salary,
            command.WeekNumber,
            benefits,
            requirements,
            contact
        );

        await _jobs.Insert(opportunity);

        return opportunity;
    }
}

public class RegisterJobCommand
{
    /// <example>Acme Inc.</example>
    [Required]
    public string CompanyName { get; set; } = default!;

    [Required]
    public JobType Type { get; set; } = default!;
    [Range(1, 10)]
    public int? Semester { get; set; }
    [Required]
    public List<string> Courses { get; set; } = default!;
    [Required]
    public string Description { get; set; } = default!;

    public DateTime? LimitDate { get; set; }
    public DateTime? GraduationDate { get; set; }
    public string? IntegrationAgentName { get; set; }
    public string? Area { get; set; }
    public Workplace? Workplace { get; set; }
    [Range(1, 12)]
    public int? HoursPerDay { get; set; }
    public decimal? Salary { get; set; }
    public int? WeekNumber { get; set; }
    public bool? HasFoodVoucher { get; set; }
    public bool? HasTransportVoucher { get; set; }
    public bool? HasHealthInsurance { get; set; }
    public bool? HasLifeInsurance { get; set; }
    public EnglishLevel? EnglishLevel { get; set; }
    public string? OtherLanguages { get; set; }
    public string? SoftSkills { get; set; }
    public string? HardSkills { get; set; }
    [Url]
    public string? LinkedinUrl { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public string? EmailInstructions { get; set; }
    public string? Phone { get; set; }
    [Url]
    public string? Url { get; set; }
    public string? ExternalId { get; set; }
}
