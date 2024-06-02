using Blog_MVC.Areas.Admin.Models;
using Blog_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;



namespace Blog_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChartController : Controller
    {
        public IActionResult Index()   
        {
            return View();
        }

        public IActionResult CategoryChart()  
        {
            List<CategoryClass> list = new List<CategoryClass>();

            list.Add(new CategoryClass { categoryname = "Teknoloji", categorycount = 10 });

            list.Add(new CategoryClass { categoryname = "Yazılım", categorycount = 14 });

            list.Add(new CategoryClass { categoryname = "Spor", categorycount = 5 });


            return Json (new { jsonlist= list });
        }    
    }
}
