using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Services.Interfaces
{
    public interface IIncomeTypeService
    {
        List<IncomeType> GetIncomeTypes();
        string InsertIncomeType(IncomeType model);
        string UpdateIncomeType(IncomeType model);
        string DeleteIncomeType(int id);
    }
}