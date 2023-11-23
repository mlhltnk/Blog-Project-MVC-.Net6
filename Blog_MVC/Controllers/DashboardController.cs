using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.Controllers
{
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
