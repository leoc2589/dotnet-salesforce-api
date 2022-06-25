using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using App.Interfaces;
using App.Models.Options;
using App.Utils;
using System.Net.Http.Headers;
using System.Text;

namespace App.Services;

public class BaseService : IBaseService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly string _uri;

    public BaseService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _configuration = configuration;
        _uri = $"{configuration.GetValue<string>("Salesforce:Account:BaseUri")}/services/data/{configuration.GetValue<string>("Salesforce:Account:ApiVersion")}";
    }

    public async Task<(bool Success, string JsonString, string Message)> CountAsync(string token, QueryOptions options)
    {
        try
        {
            var requestUri = $"{_uri}/query";

            var sql = QueryComposer.GenerateCountQuery(options);

            var query = new Dictionary<string, string> { { "q", sql } };

            Console.WriteLine(query["q"]);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString(requestUri, query));

            if (!response.IsSuccessStatusCode) return (false, null, response.ReasonPhrase);

            var jsonString = await response.Content.ReadAsStringAsync();
            return (true, jsonString, null);
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool Success, string JsonString, string Message)> ListAsync(string token, QueryOptions options)
    {
        try
        {
            var requestUri = $"{_uri}/query";

            var sql = QueryComposer.GenerateSelectQuery(options);

            var query = new Dictionary<string, string> { { "q", sql } };

            Console.WriteLine(query["q"]);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString(requestUri, query));

            if (!response.IsSuccessStatusCode) return (false, null, response.ReasonPhrase);

            var jsonString = await response.Content.ReadAsStringAsync();
            return (true, jsonString, null);
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool Success, string JsonString, string Message)> CreateAsync<T>(string token, string sobject, T request)
    {
        try
        {
            var requestUri = $"{_uri}/sobjects/{sobject}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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

    public async Task<(bool Success, string Message)> UpdateAsync<T>(string token, string sobject, string id, T request)
    {
        try
        {
            var requestUri = $"{_uri}/sobjects/{sobject}/{id}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
            });
            var response = await _httpClient.PatchAsync(requestUri, new StringContent(content, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode) return (false, response.ReasonPhrase);

            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(bool Success, string Message)> DeleteAsync(string token, string sobject, string id)
    {
        try
        {
            var requestUri = $"{_uri}/sobjects/{sobject}/{id}";

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