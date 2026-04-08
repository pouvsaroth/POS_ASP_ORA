namespace POS_ASP_ORA.Models
{
    public class CurrencyModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int DecimalPlaces { get; set; }
        public int IsBase { get; set; }
        public int Status { get; set; }
    }
}
