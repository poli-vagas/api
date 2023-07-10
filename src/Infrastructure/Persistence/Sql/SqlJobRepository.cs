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
        Job? job = await _jobs.Include(j => j.Company)
                              .Include(j => j.Courses)
                              .Include(j => j.IntegrationAgent)
                              .FirstAsync(j => j.Id == jobId);

        if (job == null) {
            throw new JobNotFoundException();
        }

        return job;
    }

    public bool TryFindByWeekNumber(int weekNumber, string description, out Job? job)
    {
        job = _jobs.Include(j => j.Company)
                   .Include(j => j.Courses)
                   .Include(j => j.IntegrationAgent)
                   .First(j => j.WeekNumber == weekNumber && j.Description == description);

        if (job == null) {
            return false;
        }

        return true;
    }

    public async Task<IEnumerable<Job>> Find(Query query)
    {
        return await Filter(query.Filter)
            .Skip(query.Page * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();
    }

    public Task<int> Count(JobFilter filter) {
        return Filter(filter).CountAsync();
    }

    private IQueryable<Job> Filter(JobFilter f) {
        return _jobs
            .Where(j => f.CompanyId.Count == 0 || f.CompanyId.Contains(j.Company.Id))
            .Where(j => f.Type.Count == 0 || f.Type.Contains(j.Type))
            .Where(j => f.CourseId.Count == 0 || j.Courses.Any(c => f.CourseId.Contains(c.Id)))
            .Where(j => f.MinLimitDate == null || j.LimitDate == null || j.LimitDate >= f.MinLimitDate)
            .Where(j => f.MaxLimitDate == null || j.LimitDate == null || j.LimitDate <= f.MaxLimitDate)
            .Where(j => f.Area.Count == 0 || (j.Area != null && f.Area.Select(a => a.ToLower()).Contains(j.Area.ToLower())))
            .Where(j => f.Workplace.Count == 0 || (j.Workplace.HasValue && f.Workplace.Contains(j.Workplace.Value)))
            .Where(j => f.MinHoursPerDay == null || (j.HoursPerDay.HasValue && j.HoursPerDay >= f.MinHoursPerDay))
            .Where(j => f.MaxHoursPerDay == null || (j.HoursPerDay.HasValue && j.HoursPerDay <= f.MaxHoursPerDay))
            .Where(j => f.MinSalary == null || (j.Salary.HasValue && j.Salary >= f.MinSalary))
            .Where(j => f.HasFoodVoucher == null || (j.Benefits.HasFoodVoucher.HasValue && f.HasFoodVoucher == j.Benefits.HasFoodVoucher))
            .Where(j => f.HasTransportVoucher == null || (j.Benefits.HasTransportVoucher.HasValue && f.HasTransportVoucher == j.Benefits.HasTransportVoucher))
            .Where(j => f.HasHealthInsurance == null || (j.Benefits.HasHealthInsurance.HasValue && f.HasHealthInsurance == j.Benefits.HasHealthInsurance))
            .Where(j => f.HasLifeInsurance == null || (j.Benefits.HasLifeInsurance.HasValue && f.HasLifeInsurance == j.Benefits.HasLifeInsurance))
            .Where(j => f.EnglishLevel.Count == 0 || (j.Requirements.EnglishLevel.HasValue && f.EnglishLevel.Contains(j.Requirements.EnglishLevel.Value)))
            .Where(j => f.MinCreatedTime == null || j.CreatedTime >= f.MinCreatedTime)
            .Where(j => f.MaxCreatedTime == null || j.CreatedTime <= f.MaxCreatedTime)
            .Include(j => j.Company)
            .Include(j => j.Courses)
            .Include(j => j.IntegrationAgent);
    }
}
