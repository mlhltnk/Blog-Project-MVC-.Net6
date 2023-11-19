using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Blog_MVC.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        public IActionResult Index()
        {
            var values = bm.GetBlogListWithCategory();
            return View(values);
        }

        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;
            var values = bm.GetBlogByID(id);
            return View(values);
        }

        public IActionResult BlogListByWriter()   //yazara göre blog listesi getir
        {
            var values= bm.GetBlogListByWriter(1);
            return View(values);
        }

        [HttpGet]
        public IActionResult BlogAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BlogAdd(Blog p)
        {
            BlogValidator bv = new BlogValidator();    //validatörü burada newledik kullanmak için
            ValidationResult results = bv.Validate(p);     //pdan gelen değerleri validate et

            if (results.IsValid)  //işlem geçerliyse
            {
                p.BlogStatus = true;
                p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.WriterId = 1;
                bm.TAdd(p);
                return RedirectToAction("BlogListByWriter", "Blog");  //BlogListByWriter actionu 'Blog'controller içinde
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
