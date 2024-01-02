using Blog_MVC.Models;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.Drawing.Charts;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Blog_MVC.Controllers
{

	public class WriterController : Controller
	{
		WriterManager wm = new WriterManager(new EfWriterRepository());


		private readonly UserManager<AppUser> _userManager;   //writer tablosu yerine Appuser ani identity tablosunu kulllanacağım

        public WriterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
		public IActionResult Index()
		{        
														//login ile birlikte login olan kişinin verilerini taşıma işlemi
            var usermail = User.Identity.Name;          //User.Identity;login olan kullanıcıyla ilgili kimlik bilgilerini temsil eden bir nesneyi ifade eder. Identity yapısından gelmektedir
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
		public async Task<IActionResult> WriterEditProfile()
		{
			// 1. YÖNTEM----->VERİLER WRİTER YERİNE APPUSER TABLOSU KULLANILARAK VERİLER GETİRİLDİ ANCAK "FİNDBYNAMEASYNC METODU" KULLANILDI!*******************

			var values = await _userManager.FindByNameAsync(User.Identity.Name);    //asenkron bir şekilde (await) kullanıcıyı adıyla bulma işlemidir. FindByNameAsync kullanma sebebim sistemde kullanıcı adıma göre işlem gerçekleştireceğim)
																					//User.Identity.Name ifadesi, sisteme giriş yapacak kullanıcı adını temsil eder
			UserUpdateViewModel model = new UserUpdateViewModel();

			model.mail = values.Email;
			model.namesurname = values.NameSurname;
			model.imageurl = values.ImageUrl;
            model.username= values.UserName;


            /*2. YÖNTEM----->VERİLER WRİTER YERİNE APPUSER TABLOSU KULLANILARAK VERİLER GETİRİLDİ*******************

			Context c = new Context();
			var username = User.Identity.Name;  //login olan kullanıcıyla ilgili kimlik bilgilerinden adını getirir.
            var usermail = c.Users.Where(x=>x.UserName==username).Select(y=>y.Email).FirstOrDefault();  //kullanıcı adını kullanarak mail adresini çektim  
 																									   //Users tablosunda UserName', sessiondan gelen username ile eşleşen bir kullanıcının e-posta adresini bulur			
			UserManager userManager = new UserManager(new EfUserRepository());
            var id = c.Users.Where(x => x.Email == usermail).Select(y => y.Id).FirstOrDefault();  //Users tablosunda Email'i, sessiondan gelen email ile eşleşen kullanıcının ID'sini bulur
            var values = userManager.TGetById(id); //Bulunan İD'ye göre listeleme yapar*****************************/



            /***************** VERİLERİ WRİTER'DAN ÇEKERKEN KULLANILAN KODLAR*******************
            Context c = new Context();
            var usermail = User.Identity.Name;
			var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
			var writervalues = wm.TGetById(writerID);									//logine göre id getirme işlemi
			return View(writervalues);
			***********************************************************************************/

            return View(model);
		}





        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(UserUpdateViewModel model)
        {

            //********VERİLER WRİTER YERİNE APPUSER TABLOSU KULLANILARAK VERİLER GETİRİLDİ ANCAK "FİNDBYNAMEASYNC VE UPDATEASYNC METODLARI" KULLANILDI!******
            var values = await _userManager.FindByNameAsync(User.Identity.Name);	//kullanıcı adına göre bilgilerini çeker
			values.Email = model.mail;												//modelden(kullanıcıdan) gelen güncellenmiş mail bilgisini, kullanıcının mevcut emailine(valuese) ile günceller.
            values.NameSurname = model.namesurname;
			values.ImageUrl = model.imageurl;

			values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, model.password);  //kullanıcının girdiği şifreyi hashleyen ve  bu hash değerini kullanıcın PasswordHash değerine atar. 

            var result = await _userManager.UpdateAsync(values);   //kullanıcının güncellenmiş bilgilerini veritabanında kaydeder.


            //***************** VERİLERİ WRİTER'DAN ÇEKERKEN KULLANILAN KODLAR*****************
            //WriterValidator wl = new WriterValidator();
            //ValidationResult result = wl.Validate(p);
            //wm.TUpdate(p);


            return RedirectToAction("Index","Dashboard");			
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
