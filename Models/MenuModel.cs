namespace POS_ASP_ORA.Models
{
    public class MenuModel
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Icon { get; set; }
        public int? ParentId { get; set; }
        public string ParentName { get; set; }
        public List<MenuModel> Children { get; set; } = new();
        public int DisplayOrder { get; set; }
        public int MenuLevel { get; set; }   // LEVEL
        public string TreeName { get; set; } // TREE_NAME

    }
}
