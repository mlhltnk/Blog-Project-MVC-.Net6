﻿using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        public void Delete(T t)
        {
            Context c = new Context();
            c.Remove(t);
            c.SaveChanges();
        }

        public T GetById(int id)
        {
            using var c = new Context();
            return c.Set<T>().Find(id);
        }

        public List<T> GetListAll()
        {
            using var c = new Context();
            return c.Set<T>().ToList();   //Sete bağlı olarak kullanmam lazım. Entity yok çünkü
                                        //burada direk listeleme işlemi yaptık
        }

        public void Insert(T t)
        {
            using var c = new Context();
            c.Add(t);
            c.SaveChanges();
        }

		public List<T> GetListAll(Expression<Func<T, bool>> filter)
		{
			using var c = new Context();
			return c.Set<T>().Where(filter).ToList();   //filterdan gelecek olan değere göre listeleme işlemi gerçekleştirir
                                                        //filter'ın içeriği ...Manager'larda linq sorgularla dolduruluyor.
                                                        //Buraya direk LINQ sorgusunu yazmama sebebi genel bir ifade yazmak ve herkesin kullanabilmesini sağlamak.
		}

		public void Update(T t)
        {
            using var c = new Context();
            c.Update(t);
            c.SaveChanges();
        }
    }
}
