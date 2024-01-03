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
    public class Message2Manager : IMessage2Service
    {
        IMessage2Dal _message2Dal;

        public Message2Manager(IMessage2Dal message2Dal)
        {
            _message2Dal = message2Dal;
        }

        public List<Message2> GetInboxListByWriter(int id)
        {
            return _message2Dal.GetInboxWithMessageByWriter(id);  //idden girilen değere göre yazara göre mesajları getirecek. İMZASI EFMESSAGE2REPOSİTORYDE
        }

        public List<Message2> GetSenboxListByWriter(int id)
        {
            return _message2Dal.GetSenboxWithMessageByWriter(id);
        }

        public void TAdd(Message2 t)
        {
           _message2Dal.Insert(t);        //mesaj gönderme işlemi için burayı yazdım
        }

        public void TDelete(Message2 t)
        {
            throw new NotImplementedException();
        }

        public Message2 TGetById(int id)
        {
           return _message2Dal.GetById(id);
        }

        public List<Message2> TGetlist()
        {
            return _message2Dal.GetListAll();
        }

        public void TUpdate(Message2 t)
        {
            throw new NotImplementedException();
        }
    }
}
