using Microsoft.AspNetCore.Authentication;

namespace AuthenticateApiLab.Authentication
{
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string ApiKey { get; set; }
        public string ApiKeyName { get; set; } = "X-ApiKey";
        public string? ClientId { get; set; }
        public KeyLocation KeyLocation { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                throw new ArgumentException("Invalid ApiKey configured");
            }
        }
    }

    public enum KeyLocation
    {
        Header = 0,
        Query = 1,
        HeaderOrQuery = 2,
        QueryOrHeader = 3,
    }
}
