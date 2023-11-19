using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Blog_MVC.Controllers
{

	public class WriterController : Controller
	{
	
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult WriterProfile()
		{
			return View();
		}

		
		public IActionResult WriterMail()
		{
			return View();
		}

		public IActionResult Test()
		{
			return View();
		}

		public PartialViewResult WriterNavbarPartial()
		{
			return PartialView();
		}

		public PartialViewResult WriterFooterPartial()
		{
			return PartialView();
		}
	}
}
