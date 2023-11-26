using Blog_MVC.Models;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Blog_MVC.Controllers
{

	public class WriterController : Controller
	{
		WriterManager wm = new WriterManager(new EfWriterRepository());

		[Authorize]
		public IActionResult Index()
		{
			//**login ile birlikte login olan kişinin verilerini taşıma işlemi
			var usermail = User.Identity.Name;
			ViewBag.v = usermail;
			Context c = new Context();
			var writername = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterName).FirstOrDefault();
			ViewBag.v2 = writername;
			return View();
		}

		public IActionResult WriterProfile()
		{
			return View();
		}

		
		public IActionResult WriterMail()
		{
			return View();
		}


		public IActionResult Test()
		{
			return View();
		}


		public PartialViewResult WriterNavbarPartial()
		{
			return PartialView();
		}


		public PartialViewResult WriterFooterPartial()
		{
			return PartialView();
		}



		[HttpGet]
		public IActionResult WriterEditProfile()
		{
			Context c = new Context();
            var usermail = User.Identity.Name;
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var writervalues = wm.TGetById(writerID);   //logine göre id getirme işlemi
			return View(writervalues);
		}



        [HttpPost]
        public IActionResult WriterEditProfile(Writer p)
        {
            WriterValidator wl = new WriterValidator();
			ValidationResult result = wl.Validate(p);
			if(result.IsValid)
			{
				wm.TUpdate(p);
				return RedirectToAction("Index","Dashboard");
			}
			else
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}
			}
			return View();
        }



		[HttpGet]
		public IActionResult WriterAdd()
		{ 
			return View();
		}



        [HttpPost]
        public IActionResult WriterAdd(AddProfileImage p)
        {
			Writer w = new Writer();  //yazarın özelliklerini atamak için

			//********************DOSYADAN RESİM YÜKLEME*********************

			if (p.WriterImage != null) //kullanıcı bir profil resmi yüklediyse
            {
				var extension = Path.GetExtension(p.WriterImage.FileName);    //kullanıcının yüklediği dosyanın uzantısını alır.muhtemelen bir IFormFile öğesidir,kullanıcının seçtiği dosyanın bilgilerini içerir.
                var newimagename = Guid.NewGuid() + extension;                //dosyanın yeni adını oluşturur. Guid.NewGuid(),benzersiz bir GUID oluşturur. Dosyanın adı GUID'e dosya uzantısı(extension) eklenerek oluşturulur.
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFile/", newimagename);   //dosyanın kaydedileceği dosya yolunu oluşturur. Directory.GetCurrentDirectory() mevcut çalışma dizinini alır, "wwwroot/WriterImageFile/" ve yeni dosya adını birleştirerek tam dosya yolunu oluşturur.
                var stream = new FileStream(location, FileMode.Create);       //dosyanın yazılacağı FileStream'i oluşturur. location, dosyanın tam yolu olan dizini temsil eder. FileMode.Create, dosyanın oluşturulduğunu, var olan bir dosya varsa üzerine yazılacağını belirtir.
                p.WriterImage.CopyTo(stream);      //kullanıcının yüklediği dosyanın içeriğini oluşturulan FileStream'e kopyalar.kullanıcının seçtiği dosyanın içeriği, yeni oluşturulan dosya yolu ve ismi ile belirtilen konuma kopyalanır.
                w.WriterImage = newimagename;      //yazar nesnesinin WriterImage özelliğine, yeni oluşturulan dosyanın adını atar. Bu sayede, yazarın profil resmi bilgisi saklanır.
            }//*************************************************************

			//diğer yazar özelliklerinin güncellenmesi
			w.WriterMail = p.WriterMail;
			w.WriterName = p.WriterName;
			w.WriterPassword = p.WriterPassword;
			w.WriterStatus = p.WriterStatus;
			w.WriterAbout = p.WriterAbout;
			wm.TAdd(w);
			return RedirectToAction("Index", "Dashboard");
        }
    }
}
