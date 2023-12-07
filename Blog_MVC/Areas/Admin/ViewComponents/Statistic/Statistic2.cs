using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic2:ViewComponent
    {
        Context c = new Context();
        public IViewComponentResult Invoke()
        {

            ViewBag.v1 = c.Blogs.OrderByDescending(x => x.BlogID).Select(x => x.BlogTitle).Take(1).FirstOrDefault();  //en son eklenen blog başlığını getirme
            //OrderByDescending;BlogID özelliğine göre azalan sıraya göre sıralar.
            //Take(1): Bu, koleksiyondan sadece ilk elemanı alır. Burada, en yüksek BlogID'ye sahip blogun başlığını elde etmek amaçlanıyor.
            //FirstOrDefault(): Eğer koleksiyon boşsa veya null ise, varsayılan değer olan null veya türün varsayılan değeri alınır.


            ViewBag.v3 = c.Comments.Count();
            return View();
        }
    }
}
