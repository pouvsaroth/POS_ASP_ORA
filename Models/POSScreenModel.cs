namespace POS_ASP_ORA.Models
{
    public class POSScreenModel
    {
        public List<ProductTest> Products { get; set; }
    }
    public class ProductTest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
    }
}
