using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class BlogManager : IBlogService
	{
		IBlogDal _blogdal;

		public BlogManager(IBlogDal blogdal)
		{
			_blogdal = blogdal;
		}


		public List<Blog> GetBlogListWithCategory()   
        {
            return _blogdal.GetListWithCategory();
		}


        public List<Blog> GetBlogListByWriter(int id)    
                                                      
        {
            return _blogdal.GetListAll(x => x.WriterId == id);  
        }



		public List<Blog> GetBlogListWithCategoryByWriter(int id)    
                                                                     
        {
            return _blogdal.GetListWithCategoryByWriter(id);   
        }




        public List<Blog> GetLast3Blog()       
        {
			return _blogdal.GetListAll().Take(3).ToList(); ;  
		}



		public List<Blog> GetBlogByID(int id)   
        {
			return _blogdal.GetListAll(x => x.BlogID == id);

		}

	

        public void TAdd(Blog t)
        {
			_blogdal.Insert(t);
        }

        public void TDelete(Blog t)
        {
			_blogdal.Delete(t);
        }

        public void TUpdate(Blog t)
        {
          _blogdal.Update(t);
        }

		public Blog TGetById(int id)
		{
			return _blogdal.GetById(id);
		}

        public List<Blog> TGetlist()
        {
            return _blogdal.GetListAll();  
        }
    }
}
