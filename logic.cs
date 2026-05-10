using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace contact_manager
{
    class Logic
    {
        private List<Contact> _contacts = new List<Contact>();
        private string _fileName = "contact_data.json";
        
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
                AddToFile();
                Console.WriteLine($"{newContact.FirstName}, {newContact.LastName}, {newContact.PhoneNumber}");
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

        public void ShowContacts()
        {
            if (_contacts.Count == 0)
            {
                Console.WriteLine("Contact list empty");
                return;
            }
            
            Console.WriteLine("Your contacts:");

            for (int i = 0; i < _contacts.Count; i++)
            {
                Contact currentContact = _contacts[i];
                
                Console.WriteLine(currentContact);
            }
        }

        private void AddToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string convertedString = JsonSerializer.Serialize(_contacts, options);
            File.WriteAllText(_fileName, convertedString);
            
            Console.WriteLine($"Data added to file {_fileName}");
        }

        public void LoadFromFile()
        {
            if (File.Exists(_fileName))
            {
                string jsonString = File.ReadAllText(_fileName);
                _contacts = JsonSerializer.Deserialize<List<Contact>>(jsonString);
            }
        }
    }
}

