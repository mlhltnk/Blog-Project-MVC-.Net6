using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.Controllers
{
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			Context c = new Context();

            var username = User.Identity.Name;           //kullanıcı adını çektim (sisteme authentica olduğum değer)

            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();  //name'den mail adresini çektim

			var writerid = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();  //blog tablosunda writerId var o yüzden writerid'yi writers tablosundan writermaile göre writerId olacak şekilde çektik

            ViewBag.v1 = c.Blogs.Count().ToString();   //GenericRepositoryde böyle bir metot olmadığı için burada context'i çağırarak sıfırdan tanımladık
			
			ViewBag.v2 = c.Blogs.Where(x => x.WriterId == writerid).Count();   //writerid YANİ sisteme kim authentice olursa ona ait veriler(blog sayısı) gelmiş olacak
																		
			
			ViewBag.v3 = c.Categories.Count().ToString();   //kategori sayısını indexe taşıdık
			return View();
		}
	}
}
