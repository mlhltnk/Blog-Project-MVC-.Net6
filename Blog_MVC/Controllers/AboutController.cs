﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.Controllers
{
	[Authorize]
	public class AboutController : Controller
	{
		AboutManager abm = new AboutManager(new EfAboutRepository());
		public IActionResult Index()
		{
			var values = abm.Getlist();
			return View(values);
		}

		public PartialViewResult SocialMediaAbout()
		{
			
			return PartialView();
		}
	}
}