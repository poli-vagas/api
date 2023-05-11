using Microsoft.EntityFrameworkCore;
using PoliVagas.Core.Application.SearchJobs;
using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Infrastructure.Persistence;

public class SqlJobRepository : IJobRepository
{
    private SqlContext _dbContext;
    private DbSet<Job> _jobs => _dbContext.Jobs;

    public SqlJobRepository(SqlContext sqlContext)
    {
        _dbContext = sqlContext;
    }

    public async Task Insert(Job opportunity)
    {
        await _jobs.AddAsync(opportunity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<Job> FindById(Guid jobId)
    {
        Job? job = await _jobs.FindAsync(jobId);

        if (job == null) {
            throw new JobNotFoundException();
        }

        return job;
    }

    public async Task<IEnumerable<Job>> Find(Query query)
    {
        var f = query.Filter;

        return await _jobs
            .Where(o => f.CompanyId.Count == 0 || f.CompanyId.Contains(o.Company.Id))
            .Where(o => f.Type.Count == 0 || f.Type.Contains(o.Type))
            .Where(o => f.CourseId.Count == 0 || f.CourseId.Intersect(o.Courses.Select(c => c.Id)).Count() > 0)
            .Where(o => f.MinLimitDate == null || o.LimitDate == null || o.LimitDate >= f.MinLimitDate )
            .Where(o => f.MaxLimitDate == null || o.LimitDate == null || o.LimitDate <= f.MaxLimitDate )
            .Where(o => f.Area.Count == 0 || (o.Area != null && f.Area.Contains(o.Area)))
            .Where(o => f.Workplace.Count == 0 || (o.Workplace.HasValue && f.Workplace.Contains(o.Workplace.Value)))
            .Where(o => f.MinHoursPerDay == null || o.HoursPerDay.HasValue || o.HoursPerDay >= f.MinHoursPerDay)
            .Where(o => f.MaxHoursPerDay == null || o.HoursPerDay.HasValue || o.HoursPerDay <= f.MaxHoursPerDay)
            .Where(o => f.MinSalary == null || o.Salary.HasValue || o.Salary >= f.MinSalary)
            .Where(o => f.HasFoodVoucher == null || (o.Benefits.HasFoodVoucher.HasValue && f.HasFoodVoucher == o.Benefits.HasFoodVoucher))
            .Where(o => f.HasTransportVoucher == null || (o.Benefits.HasTransportVoucher.HasValue && f.HasTransportVoucher == o.Benefits.HasTransportVoucher))
            .Where(o => f.HasHealthInsurance == null || (o.Benefits.HasHealthInsurance.HasValue && f.HasHealthInsurance == o.Benefits.HasHealthInsurance))
            .Where(o => f.HasLifeInsurance == null || (o.Benefits.HasLifeInsurance.HasValue && f.HasLifeInsurance == o.Benefits.HasLifeInsurance))
            .Where(o => f.EnglishLevel.Count == 0 || (o.Requirements.EnglishLevel.HasValue && f.EnglishLevel.Contains(o.Requirements.EnglishLevel.Value)))
            .Include(o => o.Company)
            .Include(o => o.IntegrationAgent)
            .ToListAsync();
    }
}
