using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.Controllers
{
	public class CommentController : Controller
	{
		CommentManager cm = new CommentManager(new EfCommentRepository());
		public IActionResult Index()
		{
			return View();
		}

		public PartialViewResult PartialAddComment()
		{
			return PartialView();
		}

		public PartialViewResult CommentListByBLog(int id) //Blogtaki yorum listesi
		{
			var values = cm.Getlist(id);
			return PartialView(values);
		}
	}
}
