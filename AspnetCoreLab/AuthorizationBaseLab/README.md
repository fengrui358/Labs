# AuthorizationBaseLab

## 介绍

该项目用于测试 asp.net core 的基础认证功能。

## 认证

设置默认认证 scheme: `builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)`

### cookie 认证

```CSharp
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromSeconds(60);
    })
```

### jwt 认证

```CSharp
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true, // 是否验证失效时间
        ClockSkew = TimeSpan.FromSeconds(5), // 时钟偏移时间
        ValidateIssuerSigningKey = true, // 是否验证 SecretKey
        ValidAudience = "localhost",
        ValidIssuer = "localhost",
        IssuerSigningKey = secretKey
    };
});
```

### 跨站请求伪造

主要目的，偷取 cookie
