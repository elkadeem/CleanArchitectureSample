using Application.Core;
using Application.Infrastructure;
using Application.Web.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Security.Claims;

namespace Application.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                  .Enrich.FromLogContext()
                  .WriteTo.Console()
                  .CreateBootstrapLogger();
            try
            {
                Log.Information("Starting web application");
                var builder = WebApplication.CreateBuilder(args);

                builder.Host.UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.Console());

                //builder.Services.AddHttpLogging(o => { });

                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                {
                    //options.UseSqlServer("");
                    options.UseInMemoryDatabase("fortestingdb");
                });

                builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
                        .AddNegotiate();

                builder.Services.AddTransient<IClaimsTransformation, RoleClaimsTransformation>();

                builder.Services.Configure<AllowedRolesOptions>(builder.Configuration.GetSection("Authentication"));

                builder.Services.AddAuthorization(options =>
                {
                    options.AddPolicy("WaelPolicy", policy => policy.RequireUserName("MIDDLEEAST\\welkadim"));
                    options.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
                    options.FallbackPolicy = options.DefaultPolicy;
                });

                builder.Services.AddScoped<IUsersRepository, UsersRepository>();
                builder.Services.AddTransient<UsersService>();

                // Add services to the container.
                builder.Services.AddRazorPages();

                var app = builder.Build();

                //app.UseHttpLogging();
                // Configure the HTTP request pipeline.
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();

                app.MapRazorPages();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
