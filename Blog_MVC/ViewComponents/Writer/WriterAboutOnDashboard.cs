using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.ViewComponents.Writer
{
  
    public class WriterAboutOnDashboard:ViewComponent        
    {
        WriterManager wm = new WriterManager(new EfWriterRepository());

        Context c = new Context();
       
     
        public IViewComponentResult Invoke()
        {
                                                         //sisteme authentice olan kullanıcıya ait bilgileri getirme
            var username = User.Identity.Name;           //kullanıcı adını çektim


            ViewBag.veri = username;


            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();    //kullanıcı adını kullanarak mail adresini çektim  //Users tablosunda UserName özelliği username ile eşleşen bir kullanıcının e-posta adresini bulur

            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();  //Writers tablosunda WriterMail özelliği usermail ile eşleşen bir yazarın WriterID değerini bulur.

            var values = wm.GetWriterById(writerID);  //idye göre yazar getirme işlemi

            return View(values);
        }
    }
}
