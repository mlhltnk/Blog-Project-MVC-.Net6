using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.Controllers
{
    public class CategoryController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());

        //cm nesnesi ile bütün metodlarıma erişim sağlayacağım
        public IActionResult Index()
        {
            var values = cm.Getlist();
            return View(values);
        }
    }
}
