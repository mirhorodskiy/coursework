using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkLibrary
{
    public class Message
    {
        public User user;
        public string message;
        public DateTime receivingTime;
        public bool isChecked;
        public Message(User user, string message)
        {
            this.message = message;
            this.user = user;
            receivingTime = DateTime.Now;
            isChecked = false;
        }

        public override string ToString()
        {
            return message;
        }
    }
}
