using Application.Core;
using Application.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);      

            builder.Services.AddHttpLogging(o => { });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.UseSqlServer("");
                options.UseInMemoryDatabase("fortestingdb");
            });

            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddTransient<UsersService>();

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            app.UseHttpLogging();
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

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
