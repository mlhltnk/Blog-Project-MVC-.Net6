using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.ViewComponents.Writer
{
    public class WriterMessageNotification:ViewComponent
    {
		Message2Manager mm = new Message2Manager(new EfMessage2Repository());

        Context c = new Context();

        //WriterDashboard sayfasında sağ üstteki mesaj bildirimleri kısmını getiriyor
		public IViewComponentResult Invoke() //invoke=çağırma
        {
            //SİSTEME AUTHENTİCE OLANA GÖRE VERİ GETİRME**************************
            var username = User.Identity.Name;

            //Users; Appuser tablosunu ifade eder. 
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();    //kullanıcı adını kullanarak mail adresini çektim  //Users tablosunda UserName özelliği username ile eşleşen bir kullanıcının e-posta adresini bulur

            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();  //Writers tablosunda WriterMail özelliği usermail ile eşleşen bir yazarın WriterID değerini bulur.

            //**********************************************************************

            var values = mm.GetInboxListByWriter(writerID);

            return View(values);
        }

    }
}
