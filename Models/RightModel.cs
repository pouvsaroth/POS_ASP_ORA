namespace POS_ASP_ORA.Models
{
    public class RightModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int? ParentId { get; set; }

        public int IsSelected { get; set; } // 1 = checked, 0 = unchecked
        public int DisplayOrder { get; set; }
    }

    public class SaveRightModel
    {
        public int GroupId { get; set; }
        public List<int> MenuIds { get; set; }
    }
}
