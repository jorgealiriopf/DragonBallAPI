using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));

        try
        {
            var authHeaderRaw = Request.Headers["Authorization"].ToString();
            if (string.IsNullOrWhiteSpace(authHeaderRaw))
                return Task.FromResult(AuthenticateResult.Fail("Empty Authorization Header"));

            var authHeader = AuthenticationHeaderValue.Parse(authHeaderRaw);
            var credentials = Encoding.UTF8
                .GetString(Convert.FromBase64String(authHeader.Parameter ?? ""))
                .Split(':', 2);

            if (credentials.Length != 2)
                return Task.FromResult(AuthenticateResult.Fail("Invalid Credential Format"));

            var username = credentials[0];
            var password = credentials[1];

            if (username != "admin" || password != "1234")
                return Task.FromResult(AuthenticateResult.Fail("Invalid Username or Password"));

            var claims = new[] { new Claim(ClaimTypes.Name, username) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
        }
    }
}
