namespace POS_ASP_ORA.Models
{
    public class MenuModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Icon { get; set; }
        public string ParentId { get; set; }
        public List<MenuModel> Children { get; set; } = new();
        
    }
}
