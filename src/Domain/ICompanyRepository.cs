namespace PoliVagas.Core.Domain;

public interface ICompanyRepository
{
    public Task<Company> GetOrAdd(string companyName);
}
