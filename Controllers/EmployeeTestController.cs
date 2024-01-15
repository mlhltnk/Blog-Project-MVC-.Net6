using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
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
			var responseMessage = await httpClient.GetAsync("https://localhost:7093/api/Default");				//veri getirme
			var jsonString = await responseMessage.Content.ReadAsStringAsync(); //responsemessadeden gelen mesajın içeriği ReadAsStringAsync olarak karşıla
			var values = JsonConvert.DeserializeObject<List<Class1>>(jsonString);  
            return View(values);
		}
        //Deserialize ; JSON verilerini.Net nesnesine dönüştürmeye yarar
        //Serialize ; .Net nesnesini JSON verisine dönüştürmeye yarar.
        //Veriyi apiye gönderirken serialize olarak göndeririz. Veriyi alırken deserialize yaparak alırız.


        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AddEmployee(Class1 p)
        {
            var httpClient = new HttpClient();
            var jsonEmployee = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(jsonEmployee,Encoding.UTF8,"application/json");  //encoding.utf8->türkçe karakter
            var responseMessage = await httpClient.PostAsync("https://localhost:7093/api/Default", content);   //apiye bağlı olarak gerçekleşecek işlemi yazıyoruz
            if(responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(p);
            }
        }



        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("https://localhost:7093/api/Default/" + id);   //veri getirme
            if(responseMessage.IsSuccessStatusCode)
            {
                var jsonEmployee = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<Class1>(jsonEmployee);
                return View(values);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(Class1 p)
        {
            var httpClient = new HttpClient();
            var jsonEmployee =JsonConvert.SerializeObject(p);
            var content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PutAsync("https://localhost:7093/api/Default/" ,content);
            if(responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(p);
            }
        }


        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.DeleteAsync("https://localhost:7093/api/Default/" + id);   //veri getirme
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }






    public class Class1   //employee'daki property isimlerini karşılamak zorunda  //apideki verileri buraya ara katman olarak çekiyoruz
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
