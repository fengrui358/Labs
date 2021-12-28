using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"]));
builder.Services.AddSingleton(secretKey);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromSeconds(60);
    }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true, // �Ƿ���֤ʧЧʱ��
        ClockSkew = TimeSpan.FromSeconds(5), // ʱ��ƫ��ʱ��
        ValidateIssuerSigningKey = true, // �Ƿ���֤ SecretKey
        ValidAudience = "localhost",
        ValidIssuer = "localhost",
        IssuerSigningKey = secretKey
    };
});

// ����վ����α��
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-XSRF-TOKEN";
});
// ����ȫ�� Post ���� AntiforgeryToken ��֤�����Ƽ�ȫ����֤
//builder.Services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
//app.UseEndpoints(endpointBuilder =>
//{
//    endpointBuilder.MapControllers();
//});

app.Run();