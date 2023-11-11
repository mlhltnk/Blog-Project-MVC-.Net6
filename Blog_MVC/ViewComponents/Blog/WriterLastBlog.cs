using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.ViewComponents.Blog
{
	public class WriterLastBlog:ViewComponent

	{
		BlogManager bm = new BlogManager(new EfBlogRepository());
		public IViewComponentResult Invoke()
		{
			//BU SINIF BLOGREADALL SAYFASINDAKİ YAZARIN SON BLOGLARI KISMINI GETİRİYOR
			var values = bm.GetBlogListByWriter(1);
			return View(values);
		}
	}
}
