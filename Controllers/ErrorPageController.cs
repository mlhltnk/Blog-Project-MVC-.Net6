using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.Controllers
{
	public class ErrorPageController : Controller
	{
		public IActionResult Error1(int code)
		{
			return View();
		}
	}
}
