using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.ViewComponents.Category
{
	public class CategoryList:ViewComponent
	{
		
		CategoryManager cm = new CategoryManager(new EfCategoryRepository());


		//BU Sınıf BLOGREADALL SAYFASINDAKİ KATEGORİLER KISMINI GETİRİYOR
		public IViewComponentResult Invoke()
		{
			var values = cm.Getlist();
			return View(values);
		}
	}
}
