using POS_ASP_ORA.Models;
using System.Collections.Generic;

namespace POS_ASP_ORA.Services.Interfaces
{
    public interface ICompanyService
    {
        List<Company> GetCompanies();

        string InsertCompany(Company model);

        string UpdateCompany(Company model);

        string DeleteCompany(int id);
    }
}