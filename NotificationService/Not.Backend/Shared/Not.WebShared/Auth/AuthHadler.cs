using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Not.WebShared.Auth;

public class SimpleAuthHandler(IConfiguration conf,
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    System.Text.Encodings.Web.UrlEncoder encoder,
    ISystemClock clock) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder, clock)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authSection = conf.GetSection("Authentication");

        var usernameCookie = Request.Cookies?[authSection["UsernameHeader"]]?.ToString();

        if (string.IsNullOrEmpty(usernameCookie))
        {
            return Task.FromResult(AuthenticateResult.Fail("Missing X-Username header."));
        }

        var claims = new[] { new Claim(ClaimTypes.Name, usernameCookie) };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}