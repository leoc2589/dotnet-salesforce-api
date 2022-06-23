namespace App;

public static class Constants
{
    //SALEFORCE APP
    public const string BaseUrl = "https://{domainName}.my.salesforce.com";
    public const string SaleforceApiVersion = "vXX.X";
    public const string ClientId = "{clientId}";
    public const string ClientSecret = "{clientSecret}";
    public const string GrantType = "password";

    //SALESFORCE USER
    public const string Username = "{username}";
    public const string Password = "{password}";

    //ERROR MESSAGE
    public const string AuthenticationErrorMessage = "Authentication failed";

    //PAGINATION
    public const int PageSize = 2000;
}