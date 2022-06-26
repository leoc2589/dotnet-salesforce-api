using App.Interfaces;
using App.Models.Entities;
using App.Models.Maps;
using App.Models.Options;
using App.Models.Requests;
using App.Models.Responses;
using App.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App.Controllers;

[ApiController]
[Route("[controller]")]
public class QueryJobsController : ControllerBase
{
    private readonly IAuthenticationService _authService;
    private readonly IQueryJobService _queryJobService;
    private readonly ILogger<QueryJobsController> _logger;

    public QueryJobsController(
        IAuthenticationService authService,
        IQueryJobService queryJobService,
        ILogger<QueryJobsController> logger)
    {
        _authService = authService;
        _queryJobService = queryJobService;
        _logger = logger;
    }

    /// <summary>s
    /// Create query job for contacts.
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    [HttpPost]
    public async Task<ActionResult<CreateQueryJobResponse>> CreateAsync()
    {
        var (Success, JsonString, Message) = await _authService.SignInAsync();

        if (!Success) return Unauthorized();

        var authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(JsonString);

        var options = new QueryOptions { Table = TableNames.Contact, Fields = new[] { "Id", "Email" } };

        var request = new CreateQueryJobRequest { Operation = "query", Query = QueryComposer.GenerateSelectQuery(options) };

        (Success, JsonString, Message) = await _queryJobService.CreateAsync(authResponse.Access_Token, request);

        if (!Success) return BadRequest(new { Message });

        return Ok(JsonConvert.DeserializeObject<CreateQueryJobResponse>(JsonString));
    }

    /// <summary>s
    /// Contacts list by job.
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    [HttpGet("{id}/contacts")]
    public async Task<ActionResult<IEnumerable<Contact>>> GetContactsAsync(string id)
    {
        var (Success, JsonString, Message) = await _authService.SignInAsync();

        if (!Success) return Unauthorized();

        var authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(JsonString);

        var result = await _queryJobService.GetResultsAsync(authResponse.Access_Token, id);

        if (!result.Success) return BadRequest(new { result.Message });

        var records = CsvHelperUtilities.GetRecords<Contact, ContactMap>(result.CsvString);

        return Ok(records);
    }
}