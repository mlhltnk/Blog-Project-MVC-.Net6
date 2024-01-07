using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface ICommentService
	{

		void CommentAdd(Comment comment);

		//void CategoryDelete(Category category);

		//void CategoryUpdate(Category category);

		List<Comment> Getlist(int id);

        //Category GetById(int id);

        List<Comment> GetCommentListWithBlog();  //Blog->index sayfasında categoryleri getirme işlemi(blog listesini kategori ile getir)
    }
}
