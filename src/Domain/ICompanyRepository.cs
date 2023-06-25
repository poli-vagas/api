namespace PoliVagas.Core.Domain;

public interface ICompanyRepository
{
    public Task<Company> GetOrAdd(string companyName);
    public Task<IEnumerable<Company>> GetAll();
}
