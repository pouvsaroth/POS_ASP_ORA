namespace POS_ASP_ORA.Models
{
    public class PriceManagementModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal SalePrice { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public DateTime ChangedDate { get; set; }
        public string ChangedBy { get; set; }
        public string Remark { get; set; }
    }
}
