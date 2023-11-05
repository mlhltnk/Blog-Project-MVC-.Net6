using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.ViewComponents
{
	public class CommentList:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
