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


        [HttpPost]
        public IActionResult AddWriter(WriterClass w)    //Ajax ile yazar ekleme işlemi
        {
            writers.Add(w);
            var Jsonwriters = JsonConvert.SerializeObject(w);
            return Json(Jsonwriters);
        }

		[HttpPost]
		public IActionResult DeleteWriter(int id)       //Ajax ile yazar silme işlemi
		{
            var writer = writers.FirstOrDefault(w => w.Id == id);
            writers.Remove(writer);
            return Json(writer);
        }


        public IActionResult UpdateWriter(WriterClass w)   //Ajax ile yazar güncelleme işlemi
		{
            var writer = writers.FirstOrDefault(x => x.Id == w.Id);
            writer.Name= w.Name;
            var jsonWriter= JsonConvert.SerializeObject(writer);
            return Json(jsonWriter);
        }


        public IActionResult WriterList()        //yazar listesini getirir   //Aşağıdaki listeyi bir değişkene atayıp bunu json türüne convert etmem gerekiyor
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
