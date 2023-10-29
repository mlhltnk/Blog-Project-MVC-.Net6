using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Category
    {
        //property tanımlama--> erişim belirleyici türü--değişkentürü--isim--{get,set}

        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string  CategoryDescription { get; set; }
        public bool CategoryStatus { get; set; }
    }
}
