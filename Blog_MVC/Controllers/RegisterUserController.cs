using Blog_MVC.Models;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Presentation;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Blog_MVC.Controllers
{
    [AllowAnonymous]
    public class RegisterUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;   //UserManager  sisteme kayıt olmak için construtor olarak eklenmeli(identityden geliyor)


        public RegisterUserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }



        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Index(UserSignUpViewModel p)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    Email = p.Mail,   //Email, Username , NameSurname bunlar identity kütüphanesinde aspnetuser tablosundan geliyor.//p.Email vs. usersignupmodel tanımımızdan geliyor.
                    UserName = p.UserName,
                    NameSurname = p.nameSurname
                    //şifre metot çağırılırken giriliyor burada girilmiyor
                };   


                var result = await _userManager.CreateAsync(user,p.Password);                                                                              
                //UserManager<TUser> sınıfının CreateAsync metodunu kullanarak yeni bir kullanıcı hesabı oluştur.
                //user: AppUser türünde yeni oluşturulacak kullanıcının bilgilerini içerir. p.Password: Kullanıcının belirlediği şifre.Bu şifre, AppUser nesnesinin içindeki şifre özelliğine atanır.

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");  //başarılı ise login ekranına döner
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                        //("") kullanılması, genel bir hata olduğunu ve bu hatanın tüm modele uygulandığını belirtir.
                        //item.Description ise, IdentityError nesnesindeki hata açıklamasını içerir ve bu açıklama, kullanıcıya neden kayıt işleminin başarısız olduğu konusunda bilgi verir.
                    }
                }
            }
            return View(p);
        }

    }
}
