using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace contact_manager
{
    class Logic
    {
        private string IsNotEmpty(string inputSpace)
        {
            string input = "";

            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write(inputSpace);
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("You need to fill this field!");
                }
            }
            
            return input;
        }
        
        private List<Contact> _contacts = new List<Contact>();
        private string _fileName = "contact_data.json";
        
        public void AddContact()
        {
            Contact newContact = new Contact();

            newContact.FirstName = IsNotEmpty("First name: ");

            newContact.LastName = IsNotEmpty("Last name: ");
            
            newContact.PhoneNumber = IsNotEmpty("Phone number: ");
            
            newContact.EmailAddress = IsNotEmpty("Email: ");
            
            newContact.HomeAddress = IsNotEmpty("Home address: ");
            
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
                
                Console.WriteLine($"[{i + 1}] {currentContact.FirstName} {currentContact.LastName} | phone number: {currentContact.PhoneNumber} | email: {currentContact.EmailAddress} | address: {currentContact.HomeAddress} | note: {currentContact.OptionalNote}");
            }
        }

        public void DeleteContact()
        {
            Console.WriteLine("Which contact do you want to delete?");
            
            ShowContacts();

            bool _working = true;
            int _choice = -1;

            while (_working)
            {
                Console.WriteLine("Type index: ");
                _choice = Convert.ToInt32(Console.ReadLine());

                if (_choice >= _contacts.Count && _choice >= 0)
                {
                    _working = false;
                }
                
                Console.WriteLine("Index out of range");
            }
            
            _contacts.RemoveAt(_choice - 1);
            
            AddToFile();
            
            Console.WriteLine("Contact removed succesfully.");
        }

        public void EditContact()
        {
            if (_contacts.Count == 0)
            {
                Console.WriteLine("Contact list empty");
                return;
            }
            
            Console.WriteLine("Which contact do you want to edit?");
            
            ShowContacts();
            
            Console.Write("Type index: ");
            string index = Console.ReadLine();

            if (int.TryParse(index, out int choice) && choice > 0 && choice <= _contacts.Count)
            {
                Contact editingContact = _contacts[choice - 1];

                string newValue = null;

                Console.WriteLine($"First name: {editingContact.FirstName}");
                Console.Write("New first name (press ENTER to keep current value): ");
                newValue = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newValue)) editingContact.FirstName = newValue;

                Console.WriteLine($"Last name: {editingContact.LastName}");
                Console.Write("New last name (press ENTER to keep current value): ");
                newValue = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newValue)) editingContact.LastName = newValue;

                Console.WriteLine($"Phone number: {editingContact.PhoneNumber}");
                Console.Write("New phone number (press ENTER to keep current value): ");
                newValue = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newValue)) editingContact.PhoneNumber = newValue;

                Console.WriteLine($"Email address: {editingContact.EmailAddress}");
                Console.Write("New email address (press ENTER to keep current value): ");
                newValue = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newValue)) editingContact.EmailAddress = newValue;

                Console.WriteLine($"Home address: {editingContact.HomeAddress}");
                Console.Write("New home address (press ENTER to keep current value): ");
                newValue = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newValue)) editingContact.HomeAddress = newValue;

                Console.WriteLine($"Note (optionnal): {editingContact.OptionalNote}");
                Console.Write("New note (press ENTER to keep current value): ");
                newValue = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newValue)) editingContact.OptionalNote = newValue;

                ValidationContext context = new ValidationContext(editingContact);
                List<ValidationResult> results = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(editingContact, context, results, true);

                if (isValid)
                {
                    _contacts[choice - 1] = editingContact;
                    AddToFile();
                    Console.WriteLine($"Contact edited succesfully");
                }
                else
                {
                    Console.WriteLine("Can't edit contact!");
                    foreach (ValidationResult error in results)
                    {
                        Console.WriteLine($"- {error.ErrorMessage}");
                    }

                    LoadFromFile();
                }
            }
            else if (choice != 0)
            {
                Console.WriteLine("\nInvalid contact number.");
            }

        }

        private void AddToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string convertedString = JsonSerializer.Serialize(_contacts, options);
            File.WriteAllText(_fileName, convertedString);
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

