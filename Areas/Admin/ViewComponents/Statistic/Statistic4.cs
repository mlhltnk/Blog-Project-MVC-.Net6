using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic4:ViewComponent
    {
        Context c = new Context();
        public IViewComponentResult Invoke()
        {
            ViewBag.v1 = c.Admins.Where(x => x.AdminID == 2).Select(x => x.Name).FirstOrDefault();
            ViewBag.v2 = c.Admins.Where(y=>y.AdminID ==2).Select(y => y.ImageURL).FirstOrDefault();
            ViewBag.v3 = c.Admins.Where(y=>y.AdminID ==2).Select(y => y.ShortDescription).FirstOrDefault();
            return View();
        }
    }
}
