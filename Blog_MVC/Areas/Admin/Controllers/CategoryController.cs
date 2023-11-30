using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

using X.PagedList;

namespace Blog_MVC.Areas.Admin.Controllers
{
	[Area("Admin")]  //bu kontrollerda oluşturduğum actionların areadan gelmiş olduğunu programa bildirdim
	public class CategoryController : Controller
	{
		CategoryManager cm = new CategoryManager(new EfCategoryRepository());

		public IActionResult Index(int page = 1)
		{

			var values = cm.TGetlist().ToPagedList(page, 2);  //pagedlist tanımı(ilk değer;başta gelecek sayfa, ikinci değer sayfada kaç obje olacağının değeri)
			return View(values);
		}

		[HttpGet]
		public IActionResult AddCategory()
		{
			return View();
		}

        [HttpPost]
        public IActionResult AddCategory(Category p)
        {
            CategoryValidator cv = new CategoryValidator();
            ValidationResult results = cv.Validate(p);

            if (results.IsValid)
            {
                p.CategoryStatus = true;
                cm.TAdd(p);
                return RedirectToAction("Index", "Category");

            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(p);
            }
        }
    }
}
