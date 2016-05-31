namespace WIS_BusinessObjects
{
    public class RolePrivilegesBO
    {
        public int UserID { get; set; }

        public int UpdatedBy { get; set; }

        public int MenuID { get; set; }

        public int ParentMenuID { get; set; }

        public int MenuLevel { get; set; }

        public string MenuName { get; set; }

        public string MenuDescription { get; set; }

        public int ChildMenuCount { get; set; }

        public string CanView { get; set; }

        public string CanUpdate { get; set; }

        public string ModuleName { get; set; }

        public string ProjectDependent { get; set; }

        public string PAPDependent { get; set; }
    }
}