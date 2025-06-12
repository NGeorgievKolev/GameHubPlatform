using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;

namespace UserService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;

    public AuthController(AuthService auth) => _auth = auth;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var success = await _auth.RegisterAsync(dto);
        return success ? Ok("Registered") : BadRequest("Email already in use");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _auth.LoginAsync(dto);
        return token == null ? Unauthorized("Invalid credentials") : Ok(new { token });
    }
}
