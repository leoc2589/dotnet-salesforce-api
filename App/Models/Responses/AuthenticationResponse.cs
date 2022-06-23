namespace App.Models.Responses;

public class AuthenticationResponse
{
    public string Access_Token { get; set; }
    public string Instance_Url { get; set; }
    public string Id { get; set; }
    public string Token_Type { get; set; }
    public string Issued_At { get; set; }
    public string Signature { get; set; }
}