using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog_MVC.Controllers
{
	
    public class LoginController : Controller
	{
	
		public IActionResult Index()
		{
			return View();
		}




        [HttpPost]
        public async Task<IActionResult> Index(Writer p)
        {
            Context c = new Context();
            var datavalue = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword); 
            if (datavalue != null)
            {
                var claims = new List<Claim>
          {
              new Claim(ClaimTypes.Name,p.WriterMail)
          };
                var claimsidentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);  //Taleplerden bir kimlik oluşturulur. Bu kimlik, kullanıcının oturumuyla ilişkilendirilen bilgileri içerir.
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsidentity), authProperties); //oluşturulan kimliği kullanarak kullanıcıyı giriş yapmış olarak işaretler.
                                                                                                                                                       //Çerez tabanlı kimlik doğrulama kullanıldığı için bu işlem, sunucu tarafında bir çerez oluşturulup tarayıcıya gönderilerek gerçekleşir.
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

    }
}


