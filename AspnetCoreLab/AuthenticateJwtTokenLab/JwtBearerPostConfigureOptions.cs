using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace AuthenticateJwtTokenLab
{
    public class JwtBearerPostConfigureOptions : IPostConfigureOptions<JwtBearerOptions>
    {
        public void PostConfigure(string name, JwtBearerOptions options)
        {
            // 如果未设置options.TokenValidationParameters.ValidAudience，则使用options.Audience
            if (string.IsNullOrEmpty(options.TokenValidationParameters.ValidAudience) && !string.IsNullOrEmpty(options.Audience))
            {
                options.TokenValidationParameters.ValidAudience = options.Audience;
            }

            if (options.ConfigurationManager == null)
            {
                // 如果未设置MetadataAddress，则使用options.Authority+.well-known/openid-configuration

                //options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(options.MetadataAddress, new OpenIdConnectConfigurationRetriever(), new HttpDocumentRetriever(httpClient) { RequireHttps = options.RequireHttpsMetadata });
            }
        }
    }
}
