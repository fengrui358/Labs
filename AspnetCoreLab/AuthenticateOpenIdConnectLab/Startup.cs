using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticateCookieLab;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace AuthenticateOpenIdConnectLab
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddOpenIdConnect(o =>
                {
                    o.ClientId = "oidc.hybrid";
                    o.ClientSecret = "secret";

                    // ��������Authority���ͱ���ָ��MetadataAddress
                    o.Authority = "https://oidc.faasx.com/";
                    // Ĭ��ΪAuthority+".well-known/openid-configuration"
                    //o.MetadataAddress = "https://oidc.faasx.com/.well-known/openid-configuration";
                    o.RequireHttpsMetadata = false;

                    // ʹ�û����
                    o.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                    // �Ƿ�Tokens���浽AuthenticationProperties��
                    o.SaveTokens = true;
                    // �Ƿ��UserInfoEndpoint��ȡClaims
                    o.GetClaimsFromUserInfoEndpoint = true;
                    // �ڱ�ʾ���У�ʹ�õ���IdentityServer��������ClaimTypeʹ�õ���JwtClaimTypes��
                    o.TokenValidationParameters.NameClaimType = "name"; //JwtClaimTypes.Name;

                    // ���²������ж�Ӧ��Ĭ��ֵ��ͨ���������á�
                    //o.CallbackPath = new PathString("/signin-oidc");
                    //o.SignedOutCallbackPath = new PathString("/signout-callback-oidc");
                    //o.RemoteSignOutPath = new PathString("/signout-oidc");
                    //o.Scope.Add("openid");
                    //o.Scope.Add("profile");
                    //o.ResponseMode = OpenIdConnectResponseMode.FormPost; 

                    /***********************************����¼�***********************************/
                    // δ��Ȩʱ���ض���OIDC������ʱ����
                    //o.Events.OnRedirectToIdentityProvider = context => Task.CompletedTask;

                    // ��ȡ����Ȩ��ʱ����
                    //o.Events.OnAuthorizationCodeReceived = context => Task.CompletedTask;
                    // ���յ�OIDC���������ص���֤��Ϣ������Code, ID Token�ȣ�ʱ����
                    //o.Events.OnMessageReceived = context => Task.CompletedTask;
                    // ���յ�TokenEndpoint���ص���Ϣʱ����
                    //o.Events.OnTokenResponseReceived = context => Task.CompletedTask;
                    // ��֤Tokenʱ����
                    //o.Events.OnTokenValidated = context => Task.CompletedTask;
                    // ���յ�UserInfoEndpoint���ص���Ϣʱ����
                    //o.Events.OnUserInformationReceived = context => Task.CompletedTask;
                    // �����쳣ʱ����
                    //o.Events.OnAuthenticationFailed = context => Task.CompletedTask;

                    // �˳�ʱ���ض���OIDC������ʱ����
                    //o.Events.OnRedirectToIdentityProviderForSignOut = context => Task.CompletedTask;
                    // OIDC�������˳��󣬷���˻ص�ʱ����
                    //o.Events.OnRemoteSignOut = context => Task.CompletedTask;
                    // OIDC�������˳��󣬿ͻ����ض���ʱ����
                    //o.Events.OnSignedOutCallbackRedirect = context => Task.CompletedTask;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorize();

            app.Map("/profile", builder => builder.Run(async context =>
            {
                var token = await context.GetTokenAsync("Cookie");

                await context.Response.WriteHtmlAsync(async res =>
                {
                    await res.WriteAsync($"<h1>��ã���ǰ��¼�û��� {HttpResponseExtensions.HtmlEncode(context.User.Identity.Name)}</h1>");
                    await res.WriteAsync("<a class=\"btn btn-default\" href=\"/Account/Logout\">�˳�</a>");
                    await res.WriteAsync($"<h2>AuthenticationType��{context.User.Identity.AuthenticationType}</h2>");

                    await res.WriteAsync("<h2>Claims:</h2>");
                    await res.WriteTableHeader(new string[] { "Claim Type", "Value" },
                        context.User.Claims.Select(c => new string[] { c.Type, c.Value }));
                });
            }));

            // �����˳�
            app.Map("/signout", builder => builder.Run(async context =>
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await context.Response.WriteHtmlAsync(async res =>
                {
                    await res.WriteAsync($"<h1>Signed out {HttpResponseExtensions.HtmlEncode(context.User.Identity.Name)}</h1>");
                    await res.WriteAsync("<a class=\"btn btn-default\" href=\"/\">Home</a>");
                });
            }));

            // Զ���˳�
            app.Map("/signout-remote", builder => builder.Run(async context =>
            {
                await context.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties()
                {
                    RedirectUri = "/signout"
                });
            }));
        }
    }
}
