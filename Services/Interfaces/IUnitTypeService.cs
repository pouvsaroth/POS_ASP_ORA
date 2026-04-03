using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Services.Interfaces
{
    public interface IUnitTypeService
    {
        List<UnitType> GetUnitTypes();
        string InsertUnitType(UnitType model);
        string UpdateUnitType(UnitType model);
        string DeleteUnitType(int id);
    }
}