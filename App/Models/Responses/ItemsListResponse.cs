namespace App.Models.Responses;

public class ItemsListResponse
{
    public int TotalSize { get; set; }
    public bool Done { get; set; }
    public string NextRecordsUrl { get; set; }
}