namespace App.Interfaces;

public interface IAuthenticationService
{
    Task<(bool Success, string JsonString, string Message)> AuthenticateAsync();
}