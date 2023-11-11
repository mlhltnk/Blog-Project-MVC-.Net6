using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.Controllers
{
	public class RegisterController : Controller
	{
		WriterManager wm = new WriterManager(new EfWriterRepository());

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(Writer p)
		{
			WriterValidator wv = new WriterValidator();    //validatörü burada newledik kullanmak için
			ValidationResult results = wv.Validate(p);     //p(writer)dan gelen değerleri validate et

			if(results.IsValid)  //işlem geçerliyse
			{
				p.WriterStatus = true;
				p.WriterAbout = "Deneme Test";
				wm.WriterAdd(p);
				return RedirectToAction("Index", "Blog");  //index actionu 'Blog'controller içinde
			}
			else
			{
				foreach (var item in results.Errors) //işlem geçerli değilse
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);  //model durumuna hatayı veren properynin ismi ve hatanın mesajını ekle
				}
			}
		
			return View();
		}
	}
}
