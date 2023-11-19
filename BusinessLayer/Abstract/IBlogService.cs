using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IBlogService:IGenericService<Blog>
	{
		//void BlogAdd(Blog blog);

		//void BlogDelete(Blog blog);

		//void BlogUpdate(Blog blog);

		//List<Blog> Getlist();

		//Blog GetById(int id);

		List<Blog> GetBlogListWithCategory();  //Blog->index sayfasında categoryleri getirme işlemi(blog listesini kategori ile getir)

		List<Blog> GetBlogListByWriter(int id);  //bloglistesini yazarla getir ama dışardan bir id parametresi al ona göre getir öyle getir

	}
}
