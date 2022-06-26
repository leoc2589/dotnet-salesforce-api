using CsvHelper.Configuration;
using App.Models.Entities;

namespace App.Models.Maps;

public class ContactMap : ClassMap<Contact>
{
    public ContactMap()
    {
        Map(m => m.Email).Name("Email");
        Map(m => m.Id).Name("Id");
    }
}