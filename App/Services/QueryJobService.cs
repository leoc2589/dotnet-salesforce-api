using System.Net.Http.Headers;
using System.Text;
using App.Interfaces;
using App.Models.Requests;
using App.Models.Responses;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace App.Services;

public class QueryJobService : IQueryJobService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly string _uri;

    public QueryJobService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _configuration = configuration;
        _uri = $"{configuration.GetValue<string>("Salesforce:Account:BaseUri")}/services/data/{configuration.GetValue<string>("Salesforce:Account:ApiVersion")}/jobs/query";
    }

    public async Task<(bool Success, string JsonString, string Message)> GetAsync(string token, string id)
    {
        try
        {
            var requestUri = $"{_uri}/{id}";

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

    public async Task<(bool Success, string JsonString, string Message)> CreateAsync(string token, CreateQueryJobRequest request)
    {
        try
        {
            var requestUri = $"{_uri}";

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
            var requestUri = $"{_uri}/{id}";

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
            var requestUri = $"{_uri}/{id}";

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

    public async Task<(bool Success, JobResultsResponse Response, string Message)> GetResultsAsync(string token, string id, string locator = null, int? maxRecords = null)
    {
        try
        {
            var requestUri = $"{_uri}/{id}/results";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var query = new Dictionary<string, string> { };

            if (!string.IsNullOrEmpty(locator)) query.Add("locator", locator);

            if (maxRecords.HasValue) query.Add("maxRecords", $"{maxRecords.Value}");

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString(requestUri, query));

            if (!response.IsSuccessStatusCode) return (false, null, response.ReasonPhrase);

            var jobResultsResponse = new JobResultsResponse
            {
                Locator = response.Headers.GetValues("Sforce-Locator")?.FirstOrDefault(),
                NumberOfRecords = response.Headers.GetValues("Sforce-NumberOfRecords")?.FirstOrDefault(),
                CsvString = await response.Content.ReadAsStringAsync()
            };

            return (true, jobResultsResponse, null);
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }
}