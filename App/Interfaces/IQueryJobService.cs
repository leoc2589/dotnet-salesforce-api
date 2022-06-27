using App.Models.Requests;
using App.Models.Responses;

namespace App.Interfaces;

public interface IQueryJobService
{
    Task<(bool Success, string JsonString, string Message)> GetAsync(string token, string id);
    Task<(bool Success, string JsonString, string Message)> CreateAsync(string token, CreateQueryJobRequest request);
    Task<(bool Success, string JsonString, string Message)> AbortAsync(string token, int id);
    Task<(bool Success, string Message)> DeleteAsync(string token, int id);
    Task<(bool Success, JobResultsResponse Response, string Message)> GetResultsAsync(string token, string id, string locator = null, int? maxRecords = null);
}