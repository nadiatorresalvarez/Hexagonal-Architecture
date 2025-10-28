using Microsoft.AspNetCore.Mvc;
using Lab09_NadiaTorres.Application.Interfaces;
using Lab09_NadiaTorres.Application.DTOs;

namespace Lab09_NadiaTorres.Persistense.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    public AuthController(IAuthService auth) { _auth = auth; }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
    {
        var res = await _auth.AuthenticateAsync(request);
        if (res == null) return Unauthorized();
        return Ok(res);
    }
}