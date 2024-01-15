using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;

namespace Blog_MVC.Controllers
{
    public class MessageController : Controller
    {
        Message2Manager mm = new Message2Manager(new EfMessage2Repository());
        public IActionResult InBox()  //gelen kutusu
        {
            Context c = new Context();

            var username = User.Identity.Name;

            //Users; Appuser tablosunu ifade eder. 
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();    //kullanıcı adını kullanarak mail adresini çektim  //Users tablosunda UserName özelliği username ile eşleşen bir kullanıcının e-posta adresini bulur

            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();  //Writers tablosunda WriterMail özelliği usermail ile eşleşen bir yazarın WriterID değerini bulur.
            
            var values = mm.GetInboxListByWriter(writerID);
            
            return View(values);
        }

        [HttpGet]
        public IActionResult MessageDetails(int id)
        {
            var value = mm.TGetById(id);

            return View(value);
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message2 p)
        {
            Context c = new Context();

            var username = User.Identity.Name;

            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();    //kullanıcı adını kullanarak mail adresini çektim  //Users tablosunda UserName özelliği username ile eşleşen bir kullanıcının e-posta adresini bulur

            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();  //Writers tablosunda WriterMail özelliği usermail ile eşleşen bir yazarın WriterID değerini bulur.


            var values = mm.GetInboxListByWriter(writerID);

            p.SenderID = writerID;

            p.ReceiverID = 2;       //bunu statikten kurtarmak gerekiyor???
           

            p.MessageStatus = true;

            p.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            mm.TAdd(p);

            return RedirectToAction("Inbox");
        }


        public IActionResult SendBox()
        {
            Context c = new Context();

            var username = User.Identity.Name;

            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();    //kullanıcı adını kullanarak mail adresini çektim  //Users tablosunda UserName özelliği username ile eşleşen bir kullanıcının e-posta adresini bulur

            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();  //Writers tablosunda WriterMail özelliği usermail ile eşleşen bir yazarın WriterID değerini bulur.

            var values = mm.GetSenboxListByWriter(writerID);

            return View(values);
        }

    }
}
