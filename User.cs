using System;
using System.Collections.Generic;

namespace NetworkLibrary
{
    public class User : Human, IMessage
    {
        private List<int> myInvites = new List<int>();
        private List<string> myNews = new List<string>();
        public List<int> myGroups = new List<int>();
        private List<Message> myMessages = new List<Message>();
        private int userID { get; set; }
        private static int id = -1;
        private string pseudonym { get; set; }
        public int newMessages { get; set; } = 0;

        public delegate void Network(object sender, string message);
        public event Network Notify = (sender, message) => Console.WriteLine(message);

        public User(string name, string surname, DateTime dateOfBirth, string pseudonym)
            : base(name, surname, dateOfBirth)
        {
            id++;
            userID = id;
            if (pseudonym != "")
                this.pseudonym = pseudonym;
        }

        public void sendRequest(int groupID, SocialNetwork socialNetwork)
        {
            if (myGroups.Contains(groupID))
                Notify?.Invoke(this, "You are already member of this group");
            if (groupID >= 0 && socialNetwork.groups.Count < groupID)
            {
                socialNetwork.groups[groupID].getRequest(userID, socialNetwork);
                Notify?.Invoke(this, "You have sent request to Group " + groupID);
            }
            else
                Notify?.Invoke(this, "Wrong group ID");
        }

        private void getRequest(int groupID, SocialNetwork socialNetwork, int invitorID)
        {
            myNews.Add(socialNetwork.users[invitorID].surname + 
                " is inviting you to Group " + groupID);
            myInvites.Add(groupID);
        }
        public void sendRequestToUser(int groupID, int userID, SocialNetwork socialNetwork)
        {
            if (socialNetwork.users[userID].myGroups.Contains(groupID))
                Notify?.Invoke(this, "This user is already member of Group " + groupID);
            else
            {
                socialNetwork.users[userID].getRequest(groupID, socialNetwork, this.userID);
                Notify?.Invoke(this, "The request was sent");
            }
        }

        public void checkInvites(SocialNetwork socialNetwork)
        {
            Notify?.Invoke(this, "\nMy invites: ");
            if (myInvites.Count == 0)
                Notify?.Invoke(this, "You have no invites");
            else
            {
                for (int i = 0; i < myInvites.Count; i++)
                {
                    Notify?.Invoke(this, "You was invited to Group " + myInvites[i] + ". Don't you want to join it?" +
                        "(write Yes or No)");
                    while (true)
                    {
                        string answer = Console.ReadLine();
                        if (answer.ToLower() == "yes")
                        {
                            socialNetwork.groups[myInvites[i]].members.Add(socialNetwork.users[userID]);
                            myGroups.Add(myInvites[i]);
                            myInvites.Remove(myInvites[i]);
                            i--;
                            Notify?.Invoke(this, $"You joined to Group !");
                            break;
                        }
                        else if (answer.ToLower() == "no")
                        {
                            myInvites.RemoveAt(i);
                            i--;
                            break;
                        }
                        else
                            Notify?.Invoke(this, "Wrong format, try again");
                    }
                }
            }
        }
        public void removeInvite(int id)
        {
            myInvites.Remove(id);
        }

        public void addNews(string myEvent)
        {
            myNews.Add(myEvent);
        }

        public void showNews()
        {
            Notify?.Invoke(this, "\nYour news:");
            if (myNews.Count == 0)
                Notify?.Invoke(this, "You have no news");
            else
            {
                for (int i = 0; i < myNews.Count; i++)
                    Notify?.Invoke(this, i+1 + ". \t" + myNews[i]);
            }
            myNews.Clear();
        }

        public void createGroup(SocialNetwork socialNetwork)
        {
            socialNetwork.createGroup(userID);
            myGroups.Add(socialNetwork.groups.Count - 1);
        }

        public void addGroup(int groupId)
        {
            myGroups.Add(groupId);
        }
        public string showMyGroups()
        {
            if (myGroups.Count == 0)
                return "You have no groups";
            else
            {
                string str = "My group list:\n";
                for (int i = 0; i < myGroups.Count; i++)
                    str += "Group " + myGroups[i] + "\n";
                return str;
            }
        }

        public override string ToString()
        {
            return "\nName: " + name + "\nSurname: " + surname + "\nPseudonym: " 
                + pseudonym + "\nAge: " + age + "\nID: " + userID; 
        }

        public string checkMyMessages()
        {
            if (myMessages.Count == 0)
                return "You have no messages";
            else
            {
                string mes = "";
                for (int i = 0; i < myMessages.Count; i++)
                {
                    mes += myMessages[i] + "\n" + "from " + myMessages[i].user.name + " "
                        + myMessages[i].user.surname + " time: " + myMessages[i].receivingTime;
                    myMessages[i].isChecked = true;
                }
                return mes;
            }
        }
        public string checkMyNewMessages()
        {
            if (newMessages == 0)
                return "You have no new messages";
            else
            {
                string mes = "";
                for (int i = myMessages.Count - 1 - newMessages; i < myMessages.Count; i++)
                {

                    mes += myMessages[i] + "\n" + "from " + myMessages[i].user.name + " "
                        + myMessages[i].user.surname + " time: " + myMessages[i].receivingTime;
                    myMessages[i].isChecked = true;
                }
                return mes;
            }
        }

        public void sendMessage(User user, string mes)
        {
            user.myMessages.Add(new Message(this, mes));
            user.newMessages++;
            user.myNews.Add("You have recieved new message!");
            Notify?.Invoke(this, "Message has been sent");
        }
    }
}
