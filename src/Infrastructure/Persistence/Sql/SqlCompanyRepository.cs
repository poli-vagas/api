using Microsoft.EntityFrameworkCore;
using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Infrastructure.Persistence;

public class SqlCompanyRepository : ICompanyRepository
{
    private SqlContext _dbContext;
    private DbSet<Company> _companies => _dbContext.Companies;

    public SqlCompanyRepository(SqlContext sqlContext)
    {
        _dbContext = sqlContext;
    }

    public async Task<Company> GetOrAdd(string companyName)
    {
        var company = _companies.Where(c => c.Name == companyName).FirstOrDefault();

        if (company == null) {
            company = Company.Create(companyName);
            await Insert(company);
        }

        return company;
    }

    public async Task Insert(Company Company)
    {
        await _companies.AddAsync(Company);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Company>> GetAll()
    {
        return await _companies.ToListAsync();
    }
}
