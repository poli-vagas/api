using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Application.ListCompanies;

public class ListCompaniesHandler
{
    private readonly ICompanyRepository _companies;

    public ListCompaniesHandler(ICompanyRepository companies) {
        _companies = companies;
    }

    public async Task<IEnumerable<Company>> Execute() {
        return await _companies.GetAll();
    }
}
