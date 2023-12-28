using System.ComponentModel.DataAnnotations;

namespace Blog_MVC.Models
{
    public class UserSignUpViewModel
    {
        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage = "lütfen ad soyad giriniz")]
        public string nameSurname { get; set; }

        [Display(Name = "Şifre")]
        [Compare("Password", ErrorMessage = "lütfen şifre giriniz!")]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifre Uyuşmuyor!")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Mail Adresi")]
        [Required(ErrorMessage = "lütfen mail giriniz")]
        public string Mail { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "lütfen kullanıcı adı giriniz")]
        public string UserName { get; set; }
    }
}
