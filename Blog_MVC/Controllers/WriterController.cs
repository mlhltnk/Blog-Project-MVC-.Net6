using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Blog_MVC.Controllers
{

	public class WriterController : Controller
	{
		WriterManager wm = new WriterManager(new EfWriterRepository());
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult WriterProfile()
		{
			return View();
		}

		
		public IActionResult WriterMail()
		{
			return View();
		}

		public IActionResult Test()
		{
			return View();
		}

		public PartialViewResult WriterNavbarPartial()
		{
			return PartialView();
		}

		public PartialViewResult WriterFooterPartial()
		{
			return PartialView();
		}

		[HttpGet]
		public IActionResult WriterEditProfile()
		{
			var writervalues = wm.TGetById(1);   //1 numaralı depere göre id getir
			return View(writervalues);
		}

        [HttpPost]
        public IActionResult WriterEditProfile(Writer p)
        {
            WriterValidator wl = new WriterValidator();
			ValidationResult result = wl.Validate(p);
			if(result.IsValid)
			{
				wm.TUpdate(p);
				return RedirectToAction("Index","Dashboard");
			}
			else
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
			}
			return View();
        }
    }
}
