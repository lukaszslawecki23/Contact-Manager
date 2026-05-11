using System;

namespace contact_manager
{
    class Program
    {
        static void Main()
        {
            Logic ContactManager = new Logic();
            ContactManager.LoadFromFile();

            string _choice;
            bool _working = true;
            
            Console.WriteLine("Welcome in Contact Manager!");
            
            while (_working)
            {
                Console.WriteLine("Choose option:");
                Console.WriteLine("[1] Show contacts.");
                Console.WriteLine("[2] Add contacts.");
                Console.WriteLine("[3] Delete contact.");
                Console.WriteLine("[0] Exit program.");
                _choice = Console.ReadLine();

                switch (_choice)
                {
                    case "0":
                        _working = false;
                        Console.WriteLine("Exiting program...");
                        break;
                    case "1":
                        ContactManager.ShowContacts();
                        break;
                    case "2":
                        ContactManager.AddContact();
                        break;
                    case "3":
                        ContactManager.DeleteContact();
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }
}