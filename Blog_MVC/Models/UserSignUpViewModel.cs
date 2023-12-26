using System.ComponentModel.DataAnnotations;

namespace Blog_MVC.Models
{
    public class UserSignUpViewModel
    {
        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage = "lütfen ad soyad giriniz")]
        public string nameSurname { get; set; }

        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor!")]
        public string Password { get; set; }

        [Display(Name = "Mail Adresi")]
        [Required(ErrorMessage = "lütfen mail giriniz")]
        public string Mail { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "lütfen kullanıcı adı giriniz")]
        public string UserName { get; set; }
    }
}
