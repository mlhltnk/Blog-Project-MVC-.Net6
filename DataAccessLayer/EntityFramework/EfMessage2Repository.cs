using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfMessage2Repository : GenericRepository<Message2>, IMessage2Dal
    {


        public List<Message2> GetInboxWithMessageByWriter(int id)
        {
            using (var c = new Context())
            {
                return c.Messages2.Include(x => x.WriteSender).Where(x => x.ReceiverID == id).ToList();
                //hangi entity include edilecek(dahil edilecek)se o yazılır-->kategori tablosuna ait değerleri bize getirecek ancak girdiğimiz id değeri writerid ye eşit olanları getirecek
                //Include metodu kullanılarak Message2 tablosundaki veriler ve WriteSender adındaki property sayesinde ilişkili olan writer tablosuna ait veriler de çekilir.Bu sayede her bir mesajın writer bilgilerine erişim sağlanabilir.
                //Where metodu ile sadece girilen yazarın mesajları filtrelenir.
            }


        }

        public List<Message2> GetSenboxWithMessageByWriter(int id)
        {
            using (var c = new Context())
            {
                return c.Messages2.Include(x => x.WriteReceiver).Where(x => x.SenderID == id).ToList();
            }
        }
    }
}

