using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
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
            var usermail = User.Identity.Name;
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var values = wm.GetWriterById(writerID);  //idye göre yazar getirme işlemi
            return View(values);
        }
    }
}
