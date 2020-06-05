using System;
using System.Collections.Generic;

namespace NetworkLibrary
{
    public class SocialNetwork
    {
        private string name;
        public List<Group> groups = new List<Group>();
        public List<User> users = new List<User>();

        public delegate void Network(object sender, string message);
        public event Network Notify = (sender, message) => Console.WriteLine(message);
        
        
        public SocialNetwork(string name)
        {
            this.name = name;
        }

        //public void registerUser(string name, string surname, DateTime dateOfBirth, string pseudonym)
        //{
        //    //Notify?.Invoke(this, "\nUser name:");
        //    //string name = Console.ReadLine();
        //    //Notify?.Invoke(this, "\nUser surname: ");
        //    //string surname = Console.ReadLine();
        //    ////int year;
        //    ////int month;
        //    ////int day;
        //    ////while (true)
        //    ////{
        //    ////    Notify?.Invoke(this, "\nDate of birth (write year, month and day)");
        //    ////    year = Convert.ToInt32(Console.ReadLine());
        //    ////    month = Convert.ToInt32(Console.ReadLine());
        //    ////    day = Convert.ToInt32(Console.ReadLine());
        //    ////    if (day > 31 || day < 0 || month < 0 || month > 12 ||
        //    ////        year < 0 || year > DateTime.Now.Year)
        //    ////        Notify?.Invoke(this, "Error date format, try again");
        //    ////    else
        //    ////        break;
        //    ////}
        //    //Notify?.Invoke(this, "\nDate of birth (write year, month and day)");
        //    //DateTime dateOfBirth = Convert.ToDateTime(Console.ReadLine());
        //    //Notify?.Invoke(this, "\nUser pseudonym (write none if you dont want to share it)");
        //    //string pseudonym = Console.ReadLine();
        //    User user = new User(name, surname, dateOfBirth, pseudonym);
        //    users.Add(user);
        //    Notify?.Invoke(this, $"\nUser {user.surname} {user.name} has been registrated!");
        //}

        private bool isValidate(string name, string surname)
        {
            if (name == "" || surname == "")
                return false;
            return true;
        }

        public void registerUser(string name, string surname, DateTime dateOfBirth, string pseudonym)
        {
            if (isValidate(name, surname))
            {
                User user = new User(name, surname, dateOfBirth, pseudonym);
                users.Add(user);
                Notify?.Invoke(this, $"\nUser {user.surname} {user.name} has been registrated!");
            }
            else
                Notify?.Invoke(this, "Wrong data");
        }

        public void registerUser(User user)
        {
            users.Add(user);
        }

        public void createGroup(int userId)
        {
            Group group = new Group();
            group.members.Add(users[userId]);
            groups.Add(group);
        }

        public override string ToString()
        {
            string usersInfo = "";
            for (int i = 0; i < users.Count; i++)
                usersInfo += users[i].ToString() + "\n";
            return usersInfo;
        }
    }
}
