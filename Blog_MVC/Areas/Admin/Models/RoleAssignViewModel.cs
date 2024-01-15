namespace Blog_MVC.Areas.Admin.Models
{
    public class RoleAssignViewModel
    {
        public int RoleID { get; set; }
        public string Name { get; set; }
        public bool Exists { get; set; }   //bu role bu kullanıcıda var mı? (bu kullanıcı bu rolü içeriyor mu)
    }
}
