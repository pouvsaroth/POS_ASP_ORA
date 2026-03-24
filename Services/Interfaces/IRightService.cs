using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Services.Interfaces
{
    public interface IRightService
    {
        List<RightModel> GetGroups();
        List<RightModel> GetMenusByGroup(int groupId);
        string SaveGroupMenu(int groupId, List<int> menuIds);
    }
}
