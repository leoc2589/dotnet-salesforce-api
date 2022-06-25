using App.Interfaces;
using App.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authService;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(
        IAuthenticationService authService,
        ILogger<AuthenticationController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    ///<summary>s
    /// Sign in.
    ///</summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    [HttpPost("signIn")]
    public async Task<ActionResult<AuthenticationResponse>> SignInAsync()
    {
        var (Success, JsonString, _) = await _authService.SignInAsync();

        if (!Success) return Unauthorized();

        return Ok(JsonConvert.DeserializeObject<AuthenticationResponse>(JsonString));
    }
}