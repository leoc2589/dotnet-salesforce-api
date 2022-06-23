namespace App.Models.Responses;

public class CreateQueryJobResponse
{
    public string Id { get; set; }
    public string Operation { get; set; }
    public string Object { get; set; }
    public string CreatedById { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime SystemModstamp { get; set; }
    public string State { get; set; }
    public string ConcurrencyMode { get; set; }
    public string ContentType { get; set; }
    public string ApiVersion { get; set; }
    public string LineEnding { get; set; }
    public string ColumnDelimiter { get; set; }
}