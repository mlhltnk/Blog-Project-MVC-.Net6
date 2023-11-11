using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
