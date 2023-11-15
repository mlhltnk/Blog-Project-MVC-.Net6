using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
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

		public void BlogAdd(Blog blog)
		{
			throw new NotImplementedException();
		}

		public void BlogDelete(Blog blog)
		{
			throw new NotImplementedException();
		}

		public void BlogUpdate(Blog blog)
		{
			throw new NotImplementedException();
		}

		public List<Blog> GetBlogListWithCategory()
		{
			return _blogdal.GetListWithCategory();
		}

		public Blog GetById(int id)
		{
			throw new NotImplementedException();
		}

		public List<Blog> Getlist()
		{
			return _blogdal.GetListAll();  //tüm blog listesini getir
		}

		public List<Blog> GetLast3Blog()       //bunun soyutunu yapmadık direk burada oluşturduk
		{
			return _blogdal.GetListAll().Take(3).ToList(); ;  //son 3 blogu getir(footerda kullanıyoruz)
		}

		public List<Blog> GetBlogByID(int id)  //ID'ye göre blog getir
		{
			return _blogdal.GetListAll(x => x.BlogID == id);
			//GetBlogByID metodu, _blogdal üzerinden GetListAll metodunu kullanarak belirtilen ID'ye sahip blog gönderilerini getirir ve listesini döndürür
			//Burada; belirtilen ID'ye sahip blog gönderilerini getirilmesi işlemi, GenericRepository'de belirtilen "filter" değerine karşılık gelir.
		}

		public List<Blog> GetBlogListByWriter(int id)
		{
			return _blogdal.GetListAll(x => x.WriterId == id);  //writerid'si dışardan gelen id'ye eşit olanları listele
		}
	}
}
