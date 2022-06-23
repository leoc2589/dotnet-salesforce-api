using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace App.Utils;

public static class CsvHelperUtilities
{
    public static ICollection<T> GetRecords<T, TMap>(string csvString)
        where T : class
        where TMap : ClassMap<T>
    {
        var reader = new StringReader(csvString);
        var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
        csvReader.Context.RegisterClassMap<TMap>();
        var records = csvReader.GetRecords<T>();
        return records.ToList();
    }
}