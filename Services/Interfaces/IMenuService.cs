using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Services.Interfaces
{
    public interface IMenuService
    {
        // GET
        List<MenuModel> GetMenus();

        // INSERT
        string Insert(MenuModel model);

        // UPDATE
        string Update(MenuModel model);

        // DELETE
        string Delete(int id);
    }
}
