using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.ViewComponents.Writer
{
    public class WriterMessageNotification:ViewComponent
    {
		MessageManager mm = new MessageManager(new EfMessageRepository());
		public IViewComponentResult Invoke() //invoke=çağırma
        {
            string p;
            p = "cemal@gmail.com";  //burası ilerde session olacak
            var values = mm.GetInboxListByWriter(p);
            return View(values);
        }

    }
}
