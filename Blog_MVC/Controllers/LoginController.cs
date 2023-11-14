using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
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
					new Claim(ClaimTypes.Name, p.WriterMail)
				};

				var useridentity = new ClaimsIdentity(claims,"a");

				ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);

				await HttpContext.SignInAsync(principal);

				return RedirectToAction("Index","Writer");
			}
			else
			{
				return View();
			}
		}
	}
}

//Context c = new Context();
//var datavalue = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
//if (datavalue != null)
//{
//	HttpContext.Session.SetString("username", p.WriterMail);
//	return RedirectToAction("Index", "Writer");
//}
//else
//{
//	return View();
//}