using App.Models.Options;

namespace App.Interfaces;

public interface IBaseService
{
    Task<(bool Success, string JsonString, string Message)> CountAsync(string token, QueryOptions options);
    Task<(bool Success, string JsonString, string Message)> ListAsync(string token, QueryOptions options);
    Task<(bool Success, string JsonString, string Message)> CreateAsync<T>(string token, string sobject, T request);
    Task<(bool Success, string Message)> UpdateAsync<T>(string token, string sobject, string id, T request);
    Task<(bool Success, string Message)> DeleteAsync(string token, string sobject, string id);
}