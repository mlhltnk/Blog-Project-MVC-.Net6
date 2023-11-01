using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
