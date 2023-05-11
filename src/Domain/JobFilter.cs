namespace PoliVagas.Core.Domain;

public record JobFilter
{
    public List<Guid> CompanyId { get; set; } = new();
    public List<JobType> Type { get; set; } = new();
    public List<Guid> CourseId { get; set; } = new();
    public DateTime? MinLimitDate { get; set; }
    public DateTime? MaxLimitDate { get; set; }
    public List<string> Area { get; set; } = new();
    public List<Workplace> Workplace { get; set; } = new();
    public int? MinHoursPerDay { get; set; }
    public int? MaxHoursPerDay { get; set; }
    public decimal? MinSalary { get; set; }
    public bool? HasFoodVoucher { get; set; }
    public bool? HasTransportVoucher { get; set; }
    public bool? HasHealthInsurance { get; set; }
    public bool? HasLifeInsurance { get; set; }
    public List<EnglishLevel> EnglishLevel { get; set; } = new();
}