using App.Models.Entities;

namespace App.Models.Responses;

public class ContactsListResponse : ItemsListResponse
{
    public IEnumerable<Contact> Records { get; set; }
}