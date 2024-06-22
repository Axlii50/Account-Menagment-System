using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Account_Menagment_System.Server.Data;
using Account_Menagment_System.Server.Services;
using Account_Menagment_System.Server.Services.Interfaces;

namespace Account_Menagment_System.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Account_Menagment_SystemServerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Account_Menagment_SystemServerContext") ?? throw new InvalidOperationException("Connection string 'Account_Menagment_SystemServerContext' not found.")));

            // Add services to the container.
            builder.Services.AddScoped<IAccountService, AccountService>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
