using System;
using NetworkLibrary;

namespace MySocialNetwork
{
    class Program
    {
        static void Main(string[] args)
        {

            SocialNetwork socialNetwork = new SocialNetwork("TeleLitr");
            Console.WriteLine("\t\tWelcome to TeleLitr Social Network!");
            socialNetwork.registerUser("Nazar", "Myrhorodskyi", new DateTime(2001, 07, 02), "Nazarka");
            socialNetwork.registerUser("Alex", "Kulias", new DateTime(2001, 05, 02), "none");
            socialNetwork.registerUser("Ilia", "Palamarchuk", new DateTime(2002, 10, 30), "Palych");
            socialNetwork.registerUser("Hleb", "Zaiac", new DateTime(1999, 01, 03), "xxTiltasion");
            //Console.WriteLine(socialNetwork);

            int mainSwitch = -1;
            while (mainSwitch != 0)
            {
                Console.WriteLine("\n1.\tTo registrate new user\n" +
                                  "2.\tTo choose User\n" +
                                  "0.\tExit");

                mainSwitch = Convert.ToInt32(Console.ReadLine());
                switch (mainSwitch)
                {
                    case 1:
                        Console.WriteLine("\nUser name:");
                        string name = Console.ReadLine();
                        Console.WriteLine("\nUser surname: ");
                        string surname = Console.ReadLine();
                        Console.WriteLine("\nUser pseudonym (write none if you dont want to share it)");
                        string pseudonym = Console.ReadLine();
                        DateTime dateOfBirth;
                        try
                        {
                            Console.WriteLine("\nDate of birth (write year, month and day)");
                            dateOfBirth = Convert.ToDateTime(Console.ReadLine());
                        }
                        catch (FormatException e)
                        {
                            dateOfBirth = default;
                        }
                        socialNetwork.registerUser(name, surname, dateOfBirth, pseudonym);
                        break;
                    case 2:
                        {
                            int userSwitch = -2;

                            Console.WriteLine(socialNetwork);
                            Console.WriteLine("Choose user (enter user ID) or -1 to go back: ");
                            userSwitch = Convert.ToInt32(Console.ReadLine());
                            if (userSwitch < 0 || userSwitch > socialNetwork.users.Count - 1 && userSwitch != -1)
                            {
                                Console.WriteLine("Wrong user ID");
                            }
                            if (userSwitch == -1)
                                break;
                            int userSettingsSwitch = -1;
                            while (userSettingsSwitch != 0)
                            {
                                Console.WriteLine($"\n{socialNetwork.users[userSwitch].name} menu: ");
                                Console.WriteLine("1.\tTo show my news\n" +
                                                  "2.\tTo show information about me\n" +
                                                  "3.\tTo create group\n" +
                                                  "4.\tTo show my groups\n" +
                                                  "5.\tTo send request to group\n" +
                                                  "6.\tTo check my invites\n" +
                                                  "7.\tTo go to my group settings\n" +
                                                  "8.\tCheck all messages\n" +
                                                  "9.\tCheck new messages\n" +
                                                  "10.\tSend message\n" +
                                                  "0.\tBack");
                                userSettingsSwitch = Convert.ToInt32(Console.ReadLine());
                                switch (userSettingsSwitch)
                                {
                                    case 1:
                                        socialNetwork.users[userSwitch].showNews();
                                        break;
                                    case 2:
                                        Console.WriteLine(socialNetwork.users[userSwitch]);
                                        break;
                                    case 3:
                                        socialNetwork.users[userSwitch].createGroup(socialNetwork);
                                        break;
                                    case 4:
                                        Console.WriteLine(socialNetwork.users[userSwitch].showMyGroups());
                                        break;
                                    case 5:
                                        if (socialNetwork.groups.Count == 0)
                                            Console.WriteLine("There are no groups in network, you can create the first one!");
                                        else
                                        {
                                            Console.WriteLine("Aviable groups: ");
                                            for (int i = 0; i < socialNetwork.groups.Count; i++)
                                                Console.WriteLine("Group " + i);
                                        }
                                        Console.WriteLine("Enter group ID: ");
                                        int groupNumber = Convert.ToInt32(Console.ReadLine());
                                        if (groupNumber < 0 || groupNumber > socialNetwork.groups.Count - 1)
                                            Console.WriteLine("Wrong group ID");
                                        break;
                                    case 6:
                                        socialNetwork.users[userSwitch].checkInvites(socialNetwork);
                                        break;
                                    case 7:
                                        {
                                            if (socialNetwork.users[userSwitch].myGroups.Count == 0)
                                                Console.WriteLine("You have no groups");
                                            else
                                            {
                                                Console.WriteLine("Aviable groups: ");
                                                for (int i = 0; i < socialNetwork.users[userSwitch].myGroups.Count; i++)
                                                    Console.WriteLine("Group " + i);
                                                Console.WriteLine("Choose group (Enter group ID): ");
                                                int groupCase = -1;
                                                groupCase = Convert.ToInt32(Console.ReadLine());
                                                if (socialNetwork.users[userSwitch].myGroups.Contains(groupCase))
                                                {
                                                    Console.WriteLine("Choose option:\n" +
                                                        "1.\tTo show group requests\n" +
                                                        "2.\tTo show group members\n" +
                                                        "3.\tTo change requests\n" +
                                                        "4.\tTo invite user to group\n" +
                                                        "5.\tTo show members\n" +
                                                        "0.\tBack");
                                                    int groupSettings = -1;
                                                    groupSettings = Convert.ToInt32(Console.ReadLine());
                                                    switch (groupSettings)
                                                    {
                                                        case 1:
                                                            socialNetwork.groups[groupCase].showRequests();
                                                            break;
                                                        case 2:
                                                            Console.WriteLine(socialNetwork.groups[groupCase]);
                                                            break;
                                                        case 3:
                                                            socialNetwork.groups[groupCase].changeRequests();
                                                            break;
                                                        case 4:
                                                            Console.WriteLine("List of users in Network:");
                                                            Console.WriteLine(socialNetwork);
                                                            Console.WriteLine("Select user and write an ID: ");
                                                            int inviteID = Convert.ToInt32(Console.ReadLine());
                                                            if (inviteID < 0 || inviteID > socialNetwork.users.Count - 1)
                                                                Console.WriteLine("Wrong user ID");
                                                            else
                                                            {
                                                                socialNetwork.users[userSwitch].sendRequestToUser(groupCase, inviteID, socialNetwork);
                                                            }
                                                            break;
                                                        case 5:
                                                            Console.WriteLine(socialNetwork.groups[groupCase]);
                                                            break;
                                                        case 0:
                                                            break;
                                                        default:
                                                            Console.WriteLine("Wrong option");
                                                            break;
                                                    }

                                                }
                                                else
                                                    Console.WriteLine("You have no access to this group");
                                            }
                                            break;
                                        }
                                    case 8:
                                        Console.WriteLine(socialNetwork.users[userSwitch].checkMyMessages());
                                        break;
                                    case 9:
                                        Console.WriteLine(socialNetwork.users[userSwitch].checkMyNewMessages());
                                        break;
                                    case 10:
                                        if (socialNetwork.users[userSwitch].myGroups.Count == 0)
                                        {
                                            Console.WriteLine("First you have to join group");
                                            break;
                                        }
                                        Console.WriteLine("Who do you want to write (enter user ID)?");
                                        for (int i = 0; i < socialNetwork.users[userSwitch].myGroups.Count; i++)
                                            Console.WriteLine(socialNetwork.groups[socialNetwork.users[userSwitch].myGroups[i]]);
                                        int messageIdUser = Convert.ToInt32(Console.ReadLine());
                                        bool flag = false;
                                        for (int i = 0; i < socialNetwork.groups.Count; i++)
                                            if (socialNetwork.groups[i].members.Contains(socialNetwork.users[userSwitch])
                                                && socialNetwork.groups[i].members.Contains(socialNetwork.users[messageIdUser]))
                                                flag = true;
                                        if (flag)
                                        {
                                            Console.WriteLine("Enter your message: ");
                                            string mes = Console.ReadLine();
                                            socialNetwork.users[userSwitch].sendMessage(socialNetwork.users[messageIdUser], mes);
                                        }
                                        else
                                            Console.WriteLine("You can write only to group members");
                                        break;
                                    case 0:
                                        break;
                                    default:
                                        Console.WriteLine("Wrong option, try again");
                                        break;
                                }
                            }
                            break;
                        }

                    case 0:
                        Console.WriteLine("Goodbye! Have a nice day!");
                        break;
                    default:
                        Console.WriteLine("Wrong option, try again!");
                        break;

                }
            }
        }
    }
}
