using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Message2
    {
        [Key]
        public int MessageID { get; set; }

        public int? SenderID { get; set; }  //foreing key
        public int? ReceiverID { get; set; }  //foreing key
        public string Subject { get; set; }
        public string? MessageDetails { get; set; }
        public DateTime MessageDate { get; set; }
        public bool MessageStatus { get; set; }

        public Writer WriteSender { get; set; } //hasOne
        public Writer WriteReceiver { get; set; }  
    }
}
