using App.Interfaces;

namespace App.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<(bool Success, string JsonString, string Message)> AuthenticateAsync()
    {
        try
        {
            var requestUri = $"{Constants.BaseUrl}/services/oauth2/token";

            var encodedContent = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "grant_type", Constants.GrantType },
                    { "client_id", Constants.ClientId },
                    { "client_secret", Constants.ClientSecret },
                    { "username", Constants.Username },
                    { "password", Constants.Password }
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