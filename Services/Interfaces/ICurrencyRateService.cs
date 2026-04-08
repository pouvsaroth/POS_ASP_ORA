using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Services.Interfaces
{
    public interface ICurrencyRateService
    {
        List<CurrencyRateModel> GetRates();

        string InsertRate(CurrencyRateModel model);

        string UpdateRate(CurrencyRateModel model);

        string DeleteRate(int id);
    }
}
