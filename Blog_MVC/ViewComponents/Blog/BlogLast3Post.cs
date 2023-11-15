﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.ViewComponents.Blog
{
	public class BlogLast3Post:ViewComponent
	{
		BlogManager bm = new BlogManager(new EfBlogRepository());
		public IViewComponentResult Invoke()
		{
			//BU SINIF footera SON 3 BLOGU GETİRİYOR
			var values = bm.GetLast3Blog();
			return View(values);
		}
	}
}
