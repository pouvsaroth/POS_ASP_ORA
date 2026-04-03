namespace POS_ASP_ORA.Models
{
    public class POSScreenModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public string ImageBasePath { get; set; }
    }   
}
