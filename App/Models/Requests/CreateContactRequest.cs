namespace App.Models.Requests;

public class CreateContactRequest
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string MobilePhone { get; set; }
}