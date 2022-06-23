using CsvHelper.Configuration;
using App.Models.Entities;

namespace App.Models.Maps;

public class ContactMap : ClassMap<Contact>
{
    public ContactMap()
    {
        Map(m => m.CreatedDate).Name("CreatedDate");
        Map(m => m.Email).Name("Email");
        Map(m => m.FirstName).Name("FirstName");
        Map(m => m.Gender).Name("Gender__c");
        Map(m => m.Id).Name("Id");
        Map(m => m.IsDeleted).Name("IsDeleted");
        Map(m => m.LastName).Name("LastName");
        Map(m => m.LastModifiedDate).Name("LastModifiedDate");
        Map(m => m.MobilePhone).Name("MobilePhone");
        Map(m => m.Phone).Name("Phone");
        Map(m => m.PhotoUrl).Name("PhotoUrl");
        Map(m => m.Name).Name("Name");
        Map(m => m.Title).Name("Title");
    }
}