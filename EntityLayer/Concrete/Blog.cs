using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Blog
    {
        [Key]
        public int BlogID { get; set; }

        public string BlogTitle { get; set; }

        public string BlogContent { get; set; }  //blogiçeriği

        public string BlogThumbnailImage { get; set; } //küçük görsel

        public string BlogImage { get; set; }
        public string BlogCreateDate { get; set; }

        public bool BlogStatus { get; set; }

    }
}
