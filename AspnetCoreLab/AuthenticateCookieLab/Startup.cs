using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AuthenticateCookieLab
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UserStore>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                // ��������Ը�����Ҫ���һЩCookie��֤��ص����ã��ڱ���ʾ����ʹ��Ĭ��ֵ�Ϳ����ˡ�
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
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

            app.Map("/Account/Login", builder => builder.Run(async context =>
            {
                if (context.Request.Method == "GET")
                {
                    await context.Response.WriteHtmlAsync(async res =>
                    {
                        await res.WriteAsync($"<form method=\"post\">");
                        await res.WriteAsync(
                            $"<input type=\"hidden\" name=\"returnUrl\" value=\"{HttpResponseExtensions.HtmlEncode(context.Request.Query["ReturnUrl"])}\"/>");
                        await res.WriteAsync(
                            $"<div class=\"form-group\"><label>�û�����<input type=\"text\" name=\"userName\" class=\"form-control\"></label></div>");
                        await res.WriteAsync(
                            $"<div class=\"form-group\"><label>���룺<input type=\"password\" name=\"password\" class=\"form-control\"></label></div>");
                        await res.WriteAsync($"<button type=\"submit\" class=\"btn btn-default\">��¼</button>");
                        await res.WriteAsync($"</form>");
                    });
                }
                else
                {
                    var userStore = context.RequestServices.GetService<UserStore>();
                    var user = userStore.FindUser(context.Request.Form["userName"], context.Request.Form["password"]);
                    if (user == null)
                    {
                        await context.Response.WriteHtmlAsync(async res =>
                        {
                            await res.WriteAsync($"<h1>�û������������</h1>");
                            await res.WriteAsync("<a class=\"btn btn-default\" href=\"/Account/Login\">����</a>");
                        });
                    }
                    else
                    {
                        var claimIdentity = new ClaimsIdentity("Cookie");
                        claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                        claimIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                        claimIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                        claimIdentity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
                        claimIdentity.AddClaim(new Claim(ClaimTypes.DateOfBirth, user.Birthday.ToString()));

                        // ʹ��JwtClaimTypes
                        //var claimIdentity = new ClaimsIdentity("Cookie", JwtClaimTypes.);
                        //claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                        //claimIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                        //claimIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                        //claimIdentity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
                        //claimIdentity.AddClaim(new Claim(ClaimTypes.DateOfBirth, user.Birthday.ToString()));

                        var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
                        // ������ע��AddAuthenticationʱ��ָ����Ĭ�ϵ�Scheme�����������Բ���ָ��Scheme��
                        await context.SignInAsync(claimsPrincipal);
                        var token = await context.GetTokenAsync("Cookie");

                        if (string.IsNullOrEmpty(context.Request.Form["ReturnUrl"])) context.Response.Redirect("/");
                        else context.Response.Redirect(context.Request.Form["ReturnUrl"]);
                    }
                }
            }));

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
                    await res.WriteTableHeader(new string[] {"Claim Type", "Value"},
                        context.User.Claims.Select(c => new string[] {c.Type, c.Value}));
                });
            }));

            app.Map("/Account/Logout", builder => builder.Run(async context =>
            {
                await context.SignOutAsync();
                context.Response.Redirect("/");
            }));

            app.Run(async context =>
            {
                await context.Response.WriteHtmlAsync(async res =>
                {
                    await res.WriteAsync($"<h2>Hello Cookie Authentication</h2>");
                    await res.WriteAsync("<a class=\"btn btn-default\" href=\"/profile\">�ҵ���Ϣ</a>");
                });
            });

            //app.UseEndpoints(endpoints => { endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); }); });
        }
    }
}
