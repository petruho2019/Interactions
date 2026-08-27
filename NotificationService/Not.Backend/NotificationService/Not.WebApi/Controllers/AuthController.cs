using Microsoft.AspNetCore.Mvc;

namespace Not.WebApi.Controllers;


[ApiController]
[Route("api/v1/{controller}")]
public class AuthController : ControllerBase
{
    [HttpGet("login")]
    public async Task<IActionResult> Login([FromQuery] string username)
    {
        System.Console.WriteLine("Login request received for username: " + username);
        if (string.IsNullOrEmpty(username))
        {
            return BadRequest(new { Message = "Username is required." });
        }

        HttpContext.Response.Cookies.Append("X-Username", username, new CookieOptions
        {
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });

        return Ok();
    }
}