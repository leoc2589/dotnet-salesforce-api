namespace App.Models.Requests;

public class CreateQueryJobRequest
{
    public string Operation { get; set; }
    public string Query { get; set; }
}