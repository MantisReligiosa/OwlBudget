using Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OwlBudget.Middleware;
using OwlBudget.Models;

namespace OwlBudget;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program), typeof(Identity));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(_ =>
            {
                _.RequireHttpsMetadata = false;
                _.TokenValidationParameters = new TokenValidationParameters
                {
                    // указывает, будет ли валидироваться издатель при валидации токена
                    ValidateIssuer = true,
                    // строка, представляющая издателя
                    ValidIssuer = AuthOptions.Issuer,

                    // будет ли валидироваться потребитель токена
                    ValidateAudience = true,
                    // установка потребителя токена
                    ValidAudience = AuthOptions.Audience,
                    // будет ли валидироваться время существования
                    ValidateLifetime = true,

                    // установка ключа безопасности
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    // валидация ключа безопасности
                    ValidateIssuerSigningKey = true
                };
            });

        services.AddHttpContextAccessor();

        services.ConfigureServices(Configuration);

        services.AddControllersWithViews();

        services.AddMediatR(typeof(Identity));
        services.AddApplicationInsightsTelemetry();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        if (bool.TryParse(Configuration["UseRequestResponseLogging"], out var isNeedToUseRequestResponseLogging) &&
            isNeedToUseRequestResponseLogging) app.UseRequestResponseLogging();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseEndpoints(_ =>
        {
            _.MapControllerRoute(
                "home",
                "{controller=Home}/{action=Index}/{id?}");
            _.MapControllerRoute(
                "api",
                "api/{controller}/{action}/{id?}");
        });

        app.Use(async (ctx, next) =>
        {
            await next();

            if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
            {
                //Re-execute the request so the user gets the error page
                var originalPath = ctx.Request.Path.Value;
                ctx.Items["originalPath"] = originalPath;
                ctx.Request.Path = "/home";
                await next();
            }
        });
    }
}