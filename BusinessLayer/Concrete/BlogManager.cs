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

		//public void BlogAdd(Blog blog)
		//{
		//	throw new NotImplementedException();
		//}

		//public void BlogDelete(Blog blog)
		//{
		//	throw new NotImplementedException();
		//}

		//public void BlogUpdate(Blog blog)
		//{
		//	throw new NotImplementedException();
		//}

		public List<Blog> GetBlogListWithCategory()   //SPESİFİK İMZASI VAR
		{
			return _blogdal.GetListWithCategory();
		}

        public List<Blog> GetBlogListByWriter(int id)    //SPESİFİK İMZASI VAR, yazara göre blog getirme işlemi
        {
            return _blogdal.GetListAll(x => x.WriterId == id);  //writerid'si dışardan gelen id'ye eşit olanları listele
        }

		public List<Blog> GetListWithCategoryByWriterBlogManager(int id)   /*İMZASI YOK DİREK BURADA OLUŞTURDUK*/
		{
			return _blogdal.GetListWithCategoryByWriter(id);   //GetListWithCategoryByWriter include metodu içeriyor
        }




        public List<Blog> GetLast3Blog()       //bunun soyutunu yapmadık direk burada oluşturduk   İMZASI YOK DİREK BURADA OLUŞTURDUK
        {
			return _blogdal.GetListAll().Take(3).ToList(); ;  //son 3 blogu getir(footerda kullanıyoruz)
		}

		public List<Blog> GetBlogByID(int id)  //ID'ye göre blog getir ancak liste formatında getirir.  İMZASI YOK DİREK BURADA OLUŞTURDUK
		{
			return _blogdal.GetListAll(x => x.BlogID == id);
			//GetBlogByID metodu, _blogdal üzerinden GetListAll metodunu kullanarak belirtilen ID'ye sahip blog gönderilerini getirir ve listesini döndürür
			//Burada; belirtilen ID'ye sahip blog gönderilerini getirilmesi işlemi, GenericRepository'de belirtilen "filter" değerine karşılık gelir.
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
            return _blogdal.GetListAll();  //tüm blog listesini getir
        }
    }
}
