using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.Controllers
{
    public class AboutController : Controller
	{
		AboutManager abm = new AboutManager(new EfAboutRepository());

	
		public IActionResult Index()
		{
			var values = abm.TGetlist();
			return View(values);
		}

		public PartialViewResult SocialMediaAbout()
		{
			
			return PartialView();
		}
	}
}
