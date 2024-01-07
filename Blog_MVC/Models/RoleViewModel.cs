using System.ComponentModel.DataAnnotations;

namespace Blog_MVC.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = " Lütfen Rol Adı Giriniz")]
        public string name { get; set; }
    }
}
