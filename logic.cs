using System;
using System.ComponentModel.DataAnnotations;

namespace contact_manager
{
    class Logic
    {
        private List<Contact> _contacts = new List<Contact>();
        
        public void AddContact()
        {
            Contact newContact = new Contact();
            
            Console.Write("First name: ");
            newContact.FirstName = Console.ReadLine();
            Console.Write("Last name: ");
            newContact.LastName = Console.ReadLine();
            Console.Write("Phone number: ");
            newContact.PhoneNumber = Console.ReadLine();
            Console.Write("Email address: ");
            newContact.EmailAddress = Console.ReadLine();
            Console.Write("Home address: ");
            newContact.HomeAddress = Console.ReadLine();
            Console.Write("Note (optional): ");
            newContact.OptionalNote = Console.ReadLine();

            ValidationContext context = new ValidationContext(newContact);
            List<ValidationResult> results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(newContact, context, results, true);

            if (isValid)
            {
                _contacts.Add(newContact);
                Console.WriteLine($"{newContact.FirstName}, {newContact.LastName}, {newContact.PhoneNumber}, {newContact.EmailAddress}, {newContact.HomeAddress}, {newContact.OptionalNote}");
            }
            else
            {
                Console.WriteLine("Can't create contact!");
                foreach (ValidationResult error in results)
                {
                    Console.WriteLine($"- {error.ErrorMessage}");
                }
            }
        }
    }
}

