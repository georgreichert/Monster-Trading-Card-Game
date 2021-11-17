using MTCG.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Server
{
    class MessageManager
    {
        private readonly List<Message> _messages;
        private int _nextMessageID = 1;

        public MessageManager ()
        {
            _messages = new List<Message>();
        }

        public User LoginUser (Credentials credentials)
        {
            throw new NotImplementedException();
        }

        public void RegisterUser (Credentials credentials)
        {
            throw new NotImplementedException();
        }

        public Message AddMessage(string messageContent)
        {
            Message message = new Message()
            {
                ID = _nextMessageID,
                Content = messageContent
            };
            _messages.Add(message);
            _nextMessageID++;
            return message;
        }

        public IEnumerable<Message> ListMessages ()
        {
            return _messages;
        }

        public void RemoveMessages (int messageID)
        {
            throw new NotImplementedException();
        }

        public Message ShowMessage (int messageID)
        {
            throw new NotImplementedException();
        }

        public void UpdateMessage (int messageID, string messageContent)
        {
            throw new NotImplementedException();
        }
    }
}
