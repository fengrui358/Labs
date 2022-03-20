using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace AuthenticateApiLab.Authentication
{
    public sealed class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private readonly IHostEnvironment _hostEnvironment;

        public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock, IHostEnvironment hostEnvironment) : base(options, logger, encoder, clock)
        {
            _hostEnvironment = hostEnvironment;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authResult = HandleAuthenticateInternal();
            return Task.FromResult(authResult);
        }

        private AuthenticateResult HandleAuthenticateInternal()
        {
            StringValues keyValues;
            var keyExists = Options.KeyLocation switch
            {
                KeyLocation.Query => Request.Query.TryGetValue(Options.ApiKeyName, out keyValues),
                KeyLocation.HeaderOrQuery => Request.Headers.TryGetValue(Options.ApiKeyName, out keyValues) ||
                                             Request.Query.TryGetValue(Options.ApiKeyName, out keyValues),
                KeyLocation.QueryOrHeader => Request.Query.TryGetValue(Options.ApiKeyName, out keyValues) ||
                                             Request.Headers.TryGetValue(Options.ApiKeyName, out keyValues),
                _ => Request.Headers.TryGetValue(Options.ApiKeyName, out keyValues),
            };
            if (!keyExists)
                return AuthenticateResult.NoResult();

            if (keyValues.ToString().Equals(Options.ApiKey))
            {
                var clientId = Options.ClientId ?? _hostEnvironment.ApplicationName;
                return AuthenticateResult.Success(
                    new AuthenticationTicket(
                        new ClaimsPrincipal(new[]
                        {
                            new ClaimsIdentity(new[]
                            {
                                new Claim(nameof(Options.ClientId), clientId, ClaimValueTypes.String, ClaimsIssuer),
                            }, Scheme.Name)
                        }), Scheme.Name)
                );
            }

            return AuthenticateResult.Fail("Invalid Api-Key");
        }
    }
}
