using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkLibrary
{
    interface IMessage
    {
        string checkMyMessages();
        string checkMyNewMessages();
        void sendMessage(User user, string mes);
    }
}
