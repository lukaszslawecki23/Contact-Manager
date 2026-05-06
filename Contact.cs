using System.ComponentModel.DataAnnotations;

namespace contact_manager;

public class Contact
{
    public string firstName;
    public string lastName;
    [StringLength(9, ErrorMessage = "Length of the phone number must be 9")]
    public string phoneNumber { get; set; }
    public string emailAddress;
    public string homeAddress;
    public string optionalNote;
}