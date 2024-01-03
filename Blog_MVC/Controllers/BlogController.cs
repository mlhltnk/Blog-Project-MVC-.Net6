using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog_MVC.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());

        [AllowAnonymous]
        public IActionResult Index()
        {
            var values = bm.GetBlogListWithCategory();
            return View(values);
        }


        [AllowAnonymous]
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;
            var values = bm.GetBlogByID(id);
            return View(values);
        }




        public IActionResult BlogListByWriter()   //yazara göre blog listesi getir  //writer tablosunu iptal edip appuser tablosunu kullandık
        {
            Context c = new Context();

            var username = User.Identity.Name;


            //Users; Appuser tablosunu ifade eder. 
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();    //kullanıcı adını kullanarak mail adresini çektim  //Users tablosunda UserName özelliği username ile eşleşen bir kullanıcının e-posta adresini bulur

            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();  //Writers tablosunda WriterMail özelliği usermail ile eşleşen bir yazarın WriterID değerini bulur.


            var values= bm.GetListWithCategoryByWriterBlogManager(writerID);             //login olanın idsine göre verisini getirme   //GetListWithCategoryByWriterblogmanager'ın bir önceki katmanında include işlemi var

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
            //***login olanın verisini getirme işlemi  
            Context c = new Context();
            var username = User.Identity.Name;

            //Users; Appuser tablosunu ifade eder. 
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();    //kullanıcı adını kullanarak mail adresini çektim  //Users tablosunda UserName özelliği username ile eşleşen bir kullanıcının e-posta adresini bulur

            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();  //Writers tablosunda WriterMail özelliği usermail ile eşleşen bir yazarın WriterID değerini bulur.

            //Login olan username ile appuser tablosundan mail adresini bulduk. SOnra bu mail adresi ile writers tablosundan writerid'yi bulduk
            //***

            BlogValidator bv = new BlogValidator();    //validatörü burada newledik kullanmak için
            ValidationResult results = bv.Validate(p);     //pdan gelen değerleri validate et
            if (results.IsValid)  //işlem geçerliyse
            {
                p.BlogStatus = true;
                p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.WriterId = writerID;
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
            //***login olanın verisini getirme işlemi
            Context c = new Context();
            var username = User.Identity.Name;

            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();    //kullanıcı adını kullanarak mail adresini çektim  //Users tablosunda UserName özelliği username ile eşleşen bir kullanıcının e-posta adresini bulur

            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();  //Writers tablosunda WriterMail özelliği usermail ile eşleşen bir yazarın WriterID değerini bulur.
         
            //***
            p.WriterId = writerID;
            p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.BlogStatus = true;
            bm.TUpdate(p);
            return RedirectToAction("BlogListByWriter");
        }
    }
}
