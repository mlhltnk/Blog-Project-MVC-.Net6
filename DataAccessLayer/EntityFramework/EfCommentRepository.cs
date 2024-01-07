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
    public class EfCommentRepository : GenericRepository<Comment>, ICommentDal
    {
        public List<Comment> GetListWithBlog()
        {
            Context c = new Context(); //yada  using (var c = new Context())
            {
               return c.Comments.Include(x => x.Blog).ToList();
                //hangi entity include edilecek(dahil edilecek)se o yazılır-->blog tablosuna ait değerleri bize getirecek
                //Include metodu kullanılarak yorums tablosundaki veriler ve blog adındaki property sayesinde ilişkili olan blog tablosuna ait veriler de çekilir. "eager loading" tekniği olarak bilinir 
            }
        }
    }
}
