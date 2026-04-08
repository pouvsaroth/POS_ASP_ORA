namespace POS_ASP_ORA.Models
{
    public class CurrencyRateModel
    {
        public int Id { get; set; }
        public int FromCurrency { get; set; }
        public int ToCurrency { get; set; }
        public decimal Rate { get; set; }

        // For display
        public string FromCode { get; set; }
        public string ToCode { get; set; }
    }
}
