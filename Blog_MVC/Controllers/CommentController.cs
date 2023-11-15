﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
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

		[HttpGet]
		public PartialViewResult PartialAddComment()
		{
			return PartialView();
		}

		[HttpPost]
		public PartialViewResult PartialAddComment(Comment p)
		{
			p.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
			p.CommentStatus = true;
			p.BlogID = 2;
			cm.CommentAdd(p);
			Response.Redirect("/Blog/BlogReadAll/" + 1);   //yorum yazıp yorumu farklı dbye kaydettirme işlemi
			return PartialView();
		}

		public PartialViewResult CommentListByBLog(int id) //Blogtaki yorum listesi
		{
			var values = cm.Getlist(id);
			return PartialView(values);
		}
	}
}