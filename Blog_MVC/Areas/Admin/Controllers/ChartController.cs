using Blog_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Blog_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChartController : Controller
    {
        public IActionResult Index()   //kategorilerin grafik üzerinde listeleneceği action olacak
        {
            return View();
        }

        public IActionResult CategoryChart()  //verilerime static olarka değer atamak için bir metot oluşturdum
        {
            List<CategoryClass> list = new List<CategoryClass>();

            list.Add(new CategoryClass { CategoryName = "Teknoloji", CategoryCount = 10 });

            list.Add(new CategoryClass { CategoryName = "Yazılım", CategoryCount = 14 });

            list.Add(new CategoryClass { CategoryName = "Spor", CategoryCount = 5 });


            return Json (new { jsonlist= list });
        }    
    }
}
