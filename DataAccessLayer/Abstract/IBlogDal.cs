using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IBlogDal:IGenericDal<Blog>
    {
        //dışarıdan başka tablolara bağlı olan entitylerde include metodu tanımlayorum. Bu entitye ait bir metot olduğu için generice tanımlamadık

        List<Blog> GetListWithCategory();    //Kategori ile beraber listeyi getir isminde bir metot tanımladık
    }
}
