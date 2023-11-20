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
	public class EfBlogRepository : GenericRepository<Blog>, IBlogDal
	{
		public List<Blog> GetListWithCategory()   //burada tanımlanma sebebi bu metotu genel olarak IGenericDal da değilde IBlogdalDal oluşturulmuş olmasıdır.
		{
			using (var c = new Context())
			{
				return c.Blogs.Include(x => x.Category).ToList();   
				
				//hangi entity include edilecek(dahil edilecek)se o yazılır-->kategori tablosuna ait değerleri bize getirecek
			}
			
		}

        public List<Blog> GetListWithCategoryByWriter(int id)
        {
            using (var c = new Context())
            {
                return c.Blogs.Include(x => x.Category).Where(x=>x.WriterId==id).ToList();

                //hangi entity include edilecek(dahil edilecek)se o yazılır-->kategori tablosuna ait değerleri bize getirecek ancak girdiğimiz id değeri writerid ye eşit olanları getirecek
            }
        }
    }
}
