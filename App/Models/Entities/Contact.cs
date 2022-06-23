namespace App.Models.Entities;

public class Contact
{
    public Attributes Attributes { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string Gender { get; set; }
    public string Id { get; set; }
    public bool IsDeleted { get; set; }
    public string LastName { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public string MobilePhone { get; set; }
    public string Phone { get; set; }
    public string PhotoUrl { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
}