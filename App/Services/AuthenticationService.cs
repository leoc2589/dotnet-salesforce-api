using App.Interfaces;

namespace App.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly string _uri;

    public AuthenticationService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _configuration = configuration;
        _uri = _configuration.GetValue<string>("Salesforce:Account:BaseUri");
    }

    public async Task<(bool Success, string JsonString, string Message)> SignInAsync()
    {
        try
        {
            var requestUri = $"{_uri}/services/oauth2/token";

            var encodedContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", Constants.GrantType },
                { "client_id", _configuration.GetValue<string>("Salesforce:Account:ClientId") },
                { "client_secret", _configuration.GetValue<string>("Salesforce:Account:ClientSecret") },
                { "username", _configuration.GetValue<string>("Salesforce:Credentials:Username") },
                { "password", _configuration.GetValue<string>("Salesforce:Credentials:Password") }
            });

            var response = await _httpClient.PostAsync(requestUri, encodedContent);

            if (!response.IsSuccessStatusCode) return (false, null, response.ReasonPhrase);

            var jsonString = await response.Content.ReadAsStringAsync();
            return (true, jsonString, null);
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }
}