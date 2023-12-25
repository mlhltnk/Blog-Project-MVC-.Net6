using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Blog_MVC.Controllers
{
	public class EmployeeTestController : Controller
	{
		public async Task<IActionResult> Index()
		{
			var httpClient= new HttpClient();  //httpclienttan new'len
			var responseMessage = await httpClient.GetAsync("https://localhost:7093/api/Default");				//localhostu karşılayacak değer
			var jsonString = await responseMessage.Content.ReadAsStringAsync(); //responsemessadeden gelen mesajın içeriği ReadAsStringAsync olarak karşıla
			var values = JsonConvert.DeserializeObject<List<Class1>>(jsonString);  
            return View(values);
		}
        //Deserialize ; JSON verilerini.Net nesnesine dönüştürmeye yarar
        //Serialize ; .Net nesnesini JSON verisine dönüştürmeye yarar.
        //Veriyi apiye gönderirken serialize olarak göndeririz. Veriyi alırken deserialize yaparak alırız.



        public IActionResult AddEmplooye()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Class1 p)
        {
            var httpClient = new HttpClient();
            var jsonEmployee = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonEmployee,Encoding.UTF8,"application/json");  //encoding.utf8->türkçe karakter
            var responseMessage = await httpClient.PostAsync("https://localhost:7093/api/Default", content);
            if(responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            else
            {
                return View(p);
            }

        }


    }






    public class Class1   //employee'daki property isimlerini karşılamak zorunda
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
