using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkLibrary
{
    public class Group
    {
        private List<User> groupRequests = new List<User>();
        public static int groupID { get; set; } = -1;
        public List<User> members = new List<User>();

        public delegate void Network(object sender, string message);
        public event Network Notify = (sender, message) => Console.WriteLine(message);

        public Group()
        {
            groupID++;
            Notify?.Invoke(this, "\nNew Group " + groupID + " was created!");
        }

        public void getRequest(int id, SocialNetwork socialNetwork)
        {
            groupRequests.Add(socialNetwork.users[id]);
        }
        private void acceptRequest(User user)
        {
            members.Add(user);
            user.removeInvite(groupID);
            groupRequests.Remove(user);
            string message = "Group " + groupID + " has accepdet your request! \tWelcome!";
            user.addGroup(groupID);
            user.addNews(message);
        }
        private void ignoreRequest(User user)
        {
            string message = "Group " + groupID.ToString() + " has ignored your request!";
            user.removeInvite(groupID);
            groupRequests.Remove(user);
        }

        public void showRequests()
        {
            Notify?.Invoke(this, "\nGroup requests:");
            if (groupRequests.Count == 0)
                Notify?.Invoke(this, "empty");
            else
            {
                for (int i = 0; i < groupRequests.Count; i++)
                    Notify?.Invoke(this, groupRequests[i].name + ' ' + groupRequests[i].surname);
                if (groupRequests.Count == 1)
                    Notify?.Invoke(this, "want to join your group");
                else
                    Notify?.Invoke(this, "wants to join your group");
            }
        }

        public void changeRequests()
        {
            if (groupRequests.Count == 0)
                Notify?.Invoke(this, "\nThere are no requests");
            else
            {
                Notify?.Invoke(this, "\nWrite Yes if you want to add user to your group," +
                    "\nwrite No to ignore user or write Back to stop checking requests\n");
                for (int i = 0; i < groupRequests.Count; i++)
                {
                    Notify?.Invoke(this, groupRequests[i].name + ' ' + groupRequests[i].surname);
                    while (true)
                    {
                        string action = Console.ReadLine();
                        if (action.ToLower() == "yes")
                        {
                            acceptRequest(groupRequests[i]);
                            i--;
                            break;
                        }
                        else if (action.ToLower() == "no")
                        {
                            ignoreRequest(groupRequests[i]);
                            i--;
                            break;
                        }
                        else if (action.ToLower() == "back")
                            break;
                        else
                            Notify?.Invoke(this, "Error format, try again");
                    }
                }
            }
        }
        public override string ToString()
        {
            string usersInfo = "\nGroup " + groupID + " members:";
            for (int i = 0; i < members.Count; i++)
                usersInfo += members[i].ToString() + "\n";
            return usersInfo;
        }
    }
}
