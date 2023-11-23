using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.Controllers
{
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			Context c = new Context();
			ViewBag.v1 = c.Blogs.Count().ToString();   //GenericRepositoryde böyle bir metot olmadığı için burada context'i çağırarak sıfırdan tanımladık
			ViewBag.v2 = c.Blogs.Where(x => x.WriterId == 1).Count();   //1 nuamralı id değerine göre blog sayısını dön
																		//bizim blog sayımızı indexe taşıdık
			ViewBag.v3 = c.Categories.Count().ToString();   //kategori sayısını indexe taşıdık
			return View();
		}
	}
}
