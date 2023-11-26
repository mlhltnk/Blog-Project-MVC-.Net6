using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.ViewComponents.Writer
{
    public class WriterMessageNotification:ViewComponent
    {
		Message2Manager mm = new Message2Manager(new EfMessage2Repository());

        //WriterDashboard sayfasında sağ üstteki mesaj bildirimleri kısmını getiriyor
		public IViewComponentResult Invoke() //invoke=çağırma
        {
            int id = 2;
            var values = mm.GetInboxListByWriter(id);
            return View(values);
        }

    }
}
