using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.Controllers
{
	public class NewsLetterController : Controller
	{
		NewsLetterManager nm = new NewsLetterManager(new EfNewsLetterRepository());

		[HttpGet]
		public PartialViewResult SubscribeMail()  //blogreadall sayfasındaki mail bültenine ekleme kısmını yapıyoruz
		{
			return PartialView();
		}

		[HttpPost]
		public IActionResult SubscribeMail(NewsLetter p)
		{
			p.MailStatus = true;
			nm.AddNewsLetter(p);
			Response.Redirect("/Blog/BlogReadAll/" + 1);   //aynı sayfada bulunan abone ol kısmında; mail adresini girerek başka bir db'ye kaydettirme işlemi
			return PartialView();



			/* public IActionResult SubscribeMail(NewsLetter p)          --> üsttekinin başka bir yöntemi 
		{
			p.MailStatus = true;
			nm.AddNewsLetter(p);
			return RedirectToAction("Index","Blog");
		}
			*/
		}
	}
}

