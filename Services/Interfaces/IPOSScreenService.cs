using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Services.Interfaces
{
    public interface IPOSScreenService
    {
        // 🔥 Save Sale (Main POS Function)
        object SaveSales(Sale model);

       
    }
}
