using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Infrastructure.Persistence;

public class InMemoryCompanyRepository : ICompanyRepository
{
    private List<Company> companies = new ();

    public Task<Company> GetOrAdd(string companyName)
    {
        var company = companies.Where(c => c.Name == companyName).FirstOrDefault();

        if (company == null) {
            company = Company.Create(companyName);
            companies.Add(company);
        }

        return Task.FromResult(company);
    }

    public Task Insert(Company company)
    {
        companies.Add(company);

        return Task.CompletedTask;
    }
}
