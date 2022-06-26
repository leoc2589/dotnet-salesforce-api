using App.Interfaces;
using App.Models.Entities;
using App.Models.Options;
using App.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IAuthenticationService _authService;
    private readonly IBaseService _baseService;
    private readonly ILogger<ContactsController> _logger;

    public ContactsController(
        IAuthenticationService authService,
        IBaseService baseService,
        ILogger<ContactsController> logger)
    {
        _authService = authService;
        _baseService = baseService;
        _logger = logger;
    }

    /// <summary>
    /// Contacts list.
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Contact>>> ListAsync()
    {
        var (Success, JsonString, Message) = await _authService.SignInAsync();

        if (!Success) return Unauthorized();

        var authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(JsonString);

        var queryOptions = new QueryOptions { Table = TableNames.Contact, Fields = new[] { "Id", "Email" } };

        (Success, JsonString, Message) = await _baseService.ListAsync(authResponse.Access_Token, queryOptions);

        if (!Success) return BadRequest(new { Message });

        var contactsListResponse = JsonConvert.DeserializeObject<ContactsListResponse>(JsonString);

        return Ok(contactsListResponse.Records);
    }
}