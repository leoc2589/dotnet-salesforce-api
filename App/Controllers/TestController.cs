using App.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly IAuthenticationService _authService;
    private readonly ILogger<TestController> _logger;

    public TestController(
        IAuthenticationService authService,
        ILogger<TestController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<string> SignIn()
    {
        return new[] { "ciao" };
    }
}