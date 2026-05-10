using System.ComponentModel.DataAnnotations;

namespace contact_manager;

public class Contact
{
    [StringLength(20, ErrorMessage = "Length of the first name must be less than 20")]
    public string FirstName { get; set; }
    [StringLength(20, ErrorMessage = "Length of the last name must be less than 20")]
    public string LastName { get; set; }
    
    [StringLength(9, ErrorMessage = "Invalid phone number")]
    [RegularExpression("^[0-9]+$", ErrorMessage = "Invalid phone number")]
    public string PhoneNumber { get; set; }
    
    [EmailAddress(ErrorMessage = "Invalida email address")]
    public string EmailAddress { get; set; }
    
    public string HomeAddress { get; set; }
    public string OptionalNote { get; set; }
}