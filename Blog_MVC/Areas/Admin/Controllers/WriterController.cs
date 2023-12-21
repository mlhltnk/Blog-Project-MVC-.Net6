using Blog_MVC.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Blog_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WriterController : Controller                    //Ajax ile verilerin consoleda listelenmesi işlemi
    {
        public IActionResult Index() 
        {
            return View();
        }


        public IActionResult GetWriterByID(int writerid)  //idye göre 1 yazar getirme işlemi metodu   //Aşağıdaki listeyi bir değişkene atayıp bunu json türüne convert etmem gerekiyor
        {
            var findWriter = writers.FirstOrDefault(x => x.Id == writerid);
            var jsonWriters = JsonConvert.SerializeObject(findWriter);
            return Json(jsonWriters);
        }



        public IActionResult WriterList()        //yazar listesini getirir      //Aşağıdaki listeyi bir değişkene atayıp bunu json türüne convert etmem gerekiyor
        {
            var jsonWriters = JsonConvert.SerializeObject(writers);
            return Json(jsonWriters);
        }


        public static List<WriterClass> writers = new List<WriterClass>
        {
            new WriterClass
            {
                Id= 1,
                Name="Ayşe"
            },
            new WriterClass
            {
                Id = 2,
                Name="Ahmet"
            },
            new WriterClass
            {
                Id= 3,
                Name="Buse"
            }
        };
    }
}
