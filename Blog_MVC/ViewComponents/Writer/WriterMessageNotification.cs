using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.ViewComponents.Writer
{
    public class WriterMessageNotification:ViewComponent
    {
        public IViewComponentResult Invoke() //invoke=çağırma
        {
           
            return View();
        }

    }
}
