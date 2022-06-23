namespace App.Models.Options;

public class QueryOptions
{
    public string Table { get; set; }
    public IEnumerable<string> Fields { get; set; }
    public string WhereClause { get; set; }
    public int? Limit { get; set; }
    public string OrderBy { get; set; }
}