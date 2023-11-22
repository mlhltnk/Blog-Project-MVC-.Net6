using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var values= bm.GetListWithCategoryByWriterBlogManager(1);
            return View(values);
        }



        [HttpGet]
        public IActionResult BlogAdd()
        {  //---------------------DROPDOWNLİST KULLANIMI---------------------------------
            CategoryManager cm = new CategoryManager(new EfCategoryRepository());
            List<SelectListItem> categoryvalues = (from x in cm.TGetlist()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,  //dropdown içinde kullanıcının gördüğü kısım
                                                       Value = x.CategoryId.ToString()  //kullanıcıya görünmeyen kısım
                                                   }).ToList();
            ViewBag.cv = categoryvalues;   //categoryvaluesden gelen değişkenleri dropdowna taşıyacağım
            //-------------------------DROPDOWNLİST KULLANIM------------------------------  
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



        public IActionResult DeleteBlog(int id)
        {
            var blogvalue = bm.TGetById(id);
            bm.TDelete(blogvalue);
            return RedirectToAction("BlogListByWriter");
        }



        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            var blogvalue = bm.TGetById(id);
            CategoryManager cm = new CategoryManager(new EfCategoryRepository());
            List<SelectListItem> categoryvalues = (from x in cm.TGetlist()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,  //dropdown içinde kullanıcının gördüğü kısım
                                                       Value = x.CategoryId.ToString()  //kullanıcıya görünmeyen kısım
                                                   }).ToList();
            ViewBag.cv = categoryvalues;   
          
            return View(blogvalue);
        }



        [HttpPost]
        public IActionResult EditBlog(Blog p)
        {
            p.WriterId = 1;
            p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.BlogStatus = true;
            bm.TUpdate(p);
            return RedirectToAction("BlogListByWriter");
        }
    }
}
