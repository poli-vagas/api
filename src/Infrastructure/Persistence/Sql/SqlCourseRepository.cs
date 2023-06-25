using Microsoft.EntityFrameworkCore;
using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Infrastructure.Persistence;

public class SqlCourseRepository : ICourseRepository
{
    private SqlContext _dbContext;
    private DbSet<Course> _courses => _dbContext.Courses;

    public SqlCourseRepository(SqlContext sqlContext)
    {
        _dbContext = sqlContext;
    }

    public async Task<Course> GetOrAdd(string companyName)
    {
        var company = _courses.Where(c => c.Name == companyName).FirstOrDefault();

        if (company == null) {
            company = Course.Create(companyName);
            await Insert(company);
        }

        return company;
    }

    private async Task Insert(Course Course)
    {
        await _courses.AddAsync(Course);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Course>> GetAll()
    {
        return await _courses.ToListAsync();
    }
}
