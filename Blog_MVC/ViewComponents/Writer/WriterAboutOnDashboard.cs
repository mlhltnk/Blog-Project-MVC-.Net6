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
       
     
        public IViewComponentResult Invoke()         //sisteme authentice olan kullanıcıya ait bilgileri getirme
        {
                                                         
            var username = User.Identity.Name;       //oturum açtığındaki kullanıcı adını username'e atadık


            ViewBag.veri = username;

                                                                                                                //Users; Appuser tablosunu ifade eder. 
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();    //kullanıcı adını kullanarak mail adresini çektim  //Users tablosunda UserName özelliği username ile eşleşen bir kullanıcının e-posta adresini bulur

            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();  //Writers tablosunda WriterMail özelliği usermail ile eşleşen bir yazarın WriterID değerini bulur.

            //Login olan username ile appuser tablosundan mail adresini bulduk. SOnra bu mail adresi ile writers tablosundan writerid'yi bulduk

            var values = wm.GetWriterById(writerID);  //idye göre yazar getirme işlemi

            return View(values);
        }
    }
}
