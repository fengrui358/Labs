using Microsoft.AspNetCore.Authentication;

namespace AuthenticateApiLab.Authentication
{
    public static class ApiKeyExtension
    {
        public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder builder)
        {
            return builder.AddApiKey(ApiKeyAuthenticationDefaults.AuthenticationSchema);
        }

        public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder builder, string schema)
        {
            return builder.AddApiKey(schema, null);
        }

        public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder builder,
            Action<ApiKeyAuthenticationOptions>? configureOptions)
        {
            return builder.AddApiKey(ApiKeyAuthenticationDefaults.AuthenticationSchema,
                configureOptions);
        }

        public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder builder, string schema,
            Action<ApiKeyAuthenticationOptions>? configureOptions)
        {
            if (configureOptions == null)
            {
                var instance = (WebApplicationBuilder)builder.Services.FirstOrDefault(s => s.ServiceType == typeof(WebApplicationBuilder))?.ImplementationInstance!;
                builder.Services.Configure<ApiKeyAuthenticationOptions>(options =>
                {
                    instance.Configuration.GetSection("ApiKeyAuthenticationOptions").Bind(options);
                    options.ApiKey = "dsa";
                });
            }
            else
            {
                builder.Services.Configure(configureOptions);
            }

            return builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(schema,
                configureOptions);
        }
    }
}
