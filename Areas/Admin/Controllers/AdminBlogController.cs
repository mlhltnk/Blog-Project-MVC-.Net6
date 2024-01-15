using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.Areas.Admin.Controllers
{
	public class AdminBlogController : Controller
	{
        BlogManager blogManager = new BlogManager(new EfBlogRepository());
        [Area("Admin")]
        public IActionResult Index()
		{
			var values = blogManager.GetBlogListWithCategory();
			return View(values);
		}
	}
}
