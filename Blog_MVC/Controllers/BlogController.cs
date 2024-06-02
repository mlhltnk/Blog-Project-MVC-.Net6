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




        public IActionResult BlogListByWriter()   
        {
            Context c = new Context();

            var username = User.Identity.Name;


       
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();    

            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();  


            var values= bm.GetBlogListWithCategoryByWriter(writerID);             
           

            return View(values);
        }






        [HttpGet]
        public IActionResult BlogAdd()
        { 
            CategoryManager cm = new CategoryManager(new EfCategoryRepository());
            List<SelectListItem> categoryvalues = (from x in cm.TGetlist()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,  
                                                       Value = x.CategoryId.ToString()  
                                                   }).ToList();
            ViewBag.cv = categoryvalues;  
            return View();
        }




        [HttpPost]
        public IActionResult BlogAdd(Blog p)
        {
           
            Context c = new Context();
            var username = User.Identity.Name;

            
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();    

            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();  

            

            BlogValidator bv = new BlogValidator();    
            ValidationResult results = bv.Validate(p);     
            if (results.IsValid)  
            {
                p.BlogStatus = true;
                p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.WriterId = writerID;
                bm.TAdd(p);
                return RedirectToAction("BlogListByWriter", "Blog");  
            }
            else
            {
                foreach (var item in results.Errors) 
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);  
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
                                                       Text = x.CategoryName,  
                                                       Value = x.CategoryId.ToString()  
                                                   }).ToList();
            ViewBag.cv = categoryvalues;   
          
            return View(blogvalue);
        }



        [HttpPost]
        public IActionResult EditBlog(Blog p)
        {
           
            Context c = new Context();
            var username = User.Identity.Name;

            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();    

            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault(); 

            //***
            p.WriterId = writerID;
            p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.BlogStatus = true;
            bm.TUpdate(p);
            return RedirectToAction("BlogListByWriter");
        }
    }
}
