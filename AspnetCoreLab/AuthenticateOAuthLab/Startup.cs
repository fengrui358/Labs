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
using Microsoft.AspNetCore.Authentication.OAuth;

namespace AuthenticateOAuthLab
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
                    options.DefaultChallengeScheme = OAuthDefaults.DisplayName;
                })
                .AddCookie()
                .AddOAuth(OAuthDefaults.DisplayName, options =>
                {
                    options.ClientId = "oauth.code";
                    options.ClientSecret = "secret";
                    options.AuthorizationEndpoint = "https://oidc.faasx.com/connect/authorize";
                    options.TokenEndpoint = "https://oidc.faasx.com/connect/token";
                    options.CallbackPath = "/signin-oauth";
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.SaveTokens = true;
                    // �¼�ִ��˳�� ��
                    // 1.����Ticket֮ǰ����
                    options.Events.OnCreatingTicket = context => Task.CompletedTask;
                    // 2.����Ticketʧ��ʱ����
                    options.Events.OnRemoteFailure = context => Task.CompletedTask;
                    // 3.Ticket�������֮�󴥷�
                    options.Events.OnTicketReceived = context => Task.CompletedTask;
                    // 4.Challengeʱ������Ĭ����ת��OAuth������
                    // options.Events.OnRedirectToAuthorizationEndpoint = context => context.Response.Redirect(context.RedirectUri);
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

            // ��Ȩ������һ��Cookie��֤�е�ʵ��һ��
            app.UseAuthorize();

            // �ҵ���Ϣ
            app.Map("/profile", builder => builder.Run(async context =>
            {
                await context.Response.WriteHtmlAsync(async res =>
                {
                    await res.WriteAsync($"<h1>��ã���ǰ��¼�û��� {HttpResponseExtensions.HtmlEncode(context.User.Identity.Name)}</h1>");
                    await res.WriteAsync("<a class=\"btn btn-default\" href=\"/Account/Logout\">�˳�</a>");

                    await res.WriteAsync($"<h2>AuthenticationType��{context.User.Identity.AuthenticationType}</h2>");

                    await res.WriteAsync("<h2>Claims:</h2>");
                    await res.WriteTableHeader(new string[] { "Claim Type", "Value" }, context.User.Claims.Select(c => new string[] { c.Type, c.Value }));

                    // �ڵ�һ���н��ܹ�HandleAuthenticateOnceAsync�������ڴ˵��ò������ж����������ġ�
                    var result = await context.AuthenticateAsync();
                    await res.WriteAsync("<h2>Tokens:</h2>");
                    await res.WriteTableHeader(new string[] { "Token Type", "Value" }, result.Properties.GetTokens().Select(token => new string[] { token.Name, token.Value }));
                });
            }));

            // �˳�
            app.Map("/Account/Logout", builder => builder.Run(async context =>
            {
                await context.SignOutAsync();
                context.Response.Redirect("/");
            }));

            // ��ҳ
            app.Run(async context =>
            {
                await context.Response.WriteHtmlAsync(async res =>
                {
                    await res.WriteAsync($"<h2>Hello OAuth Authentication</h2>");
                    await res.WriteAsync("<a class=\"btn btn-default\" href=\"/profile\">�ҵ���Ϣ</a>");
                });
            });
        }
    }
}
