using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class   //entitynin karşılığı olarak bir T parametresi veriyoruz ancak şartımız; Bu T bir classa ait bütün nitelikleri kullanacak
    {
        //tüm entitylerim buradaki metotları kullanacak yeni bir metot eklersem buraya eklemem yetecek
        void Insert(T t);

        void Delete(T t);

        void Update(T t);

        List<T> GetListAll();

        T GetById(int id);  //dışarıdan verilen idye göre getir

        List<T> GetListAll(Expression<Func<T, bool>> filter); 
        //bloga göre listeleme yapabilmem için lambda expressiondan faydalanıyorum
        //liste türünde bir T değeri tanımladık(ismi list olan)
        //şartlı srgulama ve listeleme işleminde expression kullanılıyor.
        //expression<func<t, çıkış değeri>>filter (filter parametre değeri);
    }
}
