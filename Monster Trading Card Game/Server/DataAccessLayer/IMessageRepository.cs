using MTCG.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Server.DataAccessLayer
{
    interface IMessageRepository
    {
        public IEnumerable<Message> GetMessages();
        public Message GetMessage();
        public void InsertMessage(Message message);
        public void UpdateMessage(Message message);
        public void DeleteMessage(int messageID);
    }
}
