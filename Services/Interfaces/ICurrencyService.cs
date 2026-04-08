using POS_ASP_ORA.Models;
namespace POS_ASP_ORA.Services.Interfaces
{
    public interface ICurrencyService
    {
        List<CurrencyModel> GetCurrencies();

        string InsertCurrency(CurrencyModel model);

        string UpdateCurrency(CurrencyModel model);

        string DeleteCurrency(int id);
    }
}
