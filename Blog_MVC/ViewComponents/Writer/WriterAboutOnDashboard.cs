using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.ViewComponents.Writer
{
  
    public class WriterAboutOnDashboard:ViewComponent        
    {
        WriterManager wm = new WriterManager(new EfWriterRepository());

        public IViewComponentResult Invoke()
        {

            var values = wm.GetWriterById(1);  //idye göre yazar getirme işlemi
            return View(values);
        }
    }
}
