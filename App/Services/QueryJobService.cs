using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using App.Interfaces;
using App.Models.Requests;
using System.Net.Http.Headers;
using System.Text;

namespace App.Services;

public class QueryJobService : IQueryJobService
{
    private readonly string BaseUri = $"{Constants.BaseUrl}/services/data/{Constants.SaleforceApiVersion}/jobs/query";
    private readonly HttpClient _httpClient;

    public QueryJobService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<(bool Success, string JsonString, string Message)> GetAsync(string token, string id)
    {
        try
        {
            var requestUri = $"{BaseUri}/{id}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode) return (false, null, response.ReasonPhrase);

            var jsonString = await response.Content.ReadAsStringAsync();
            return (true, jsonString, null);
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool Success, string CsvString, string Message)> GetResultsAsync(string token, string id)
    {
        try
        {
            var requestUri = $"{BaseUri}/{id}/results";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode) return (false, null, response.ReasonPhrase);

            var csvString = await response.Content.ReadAsStringAsync();
            return (true, csvString, null);
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool Success, string JsonString, string Message)> CreateAsync(string token, CreateQueryJobRequest request)
    {
        try
        {
            var requestUri = $"{BaseUri}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Console.WriteLine(request.Query);

            var content = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
            });
            var response = await _httpClient.PostAsync(requestUri, new StringContent(content, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode) return (false, null, response.ReasonPhrase);

            var jsonString = await response.Content.ReadAsStringAsync();
            return (true, jsonString, null);
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool Success, string JsonString, string Message)> AbortAsync(string token, int id)
    {
        try
        {
            var requestUri = $"{BaseUri}/{id}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = JsonConvert.SerializeObject(new { State = "Aborted" }, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
            });
            var response = await _httpClient.PatchAsync(requestUri, new StringContent(content, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode) return (false, null, response.ReasonPhrase);

            var jsonString = await response.Content.ReadAsStringAsync();
            return (true, jsonString, null);
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool Success, string Message)> DeleteAsync(string token, int id)
    {
        try
        {
            var requestUri = $"{BaseUri}/{id}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync(requestUri);

            if (!response.IsSuccessStatusCode) return (false, response.ReasonPhrase);

            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }
}