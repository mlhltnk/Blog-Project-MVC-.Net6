﻿using System;
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

        public DateTime? BlogCreateDate { get; set; }

        public bool BlogStatus { get; set; }

        public int CategoryId { get; set; }  //bire çok ilişkide çokun olduğu tarafta foreing key tanımlamamız gerekiyor.

        public Category Category { get; set; }  //bir blogun bir categorysi olabilir

		public int WriterId { get; set; }  //bire çok ilişkide çokun olduğu tarafta foreing key tanımlamamız gerekiyor.

		public Writer Writer { get; set; }  //bir writerın bir categorysi olabilir

		public List<Comment> Comments { get; set; }
    }
}