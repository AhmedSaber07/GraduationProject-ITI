
using E_Commerce.Domain.Models;
using E_Commerce.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
          //  builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<_2B_EgyptDBContext>();

            builder.Services.AddDbContext<_2B_EgyptDBContext>(
             op =>
             {
                 op.UseSqlServer(builder.Configuration.GetConnectionString("db"));
             }
             );
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
