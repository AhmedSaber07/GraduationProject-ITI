
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Services;
using E_Commerce.Application.Settings;
using E_Commerce.Domain.Models;
using E_Commerce.Infrastructure.Context;
using E_Commerce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
            builder.Services.AddSwaggerGen((options =>
            {
                options.CustomSchemaIds(type => type.ToString());
            }));
            //  builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<_2B_EgyptDBContext>();


            // registeration
            builder.Services.AddScoped<iproductService, productService>();
            builder.Services.AddScoped<iproductRepository, productRepository>();
            builder.Services.AddScoped<icategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<icategoryServices, CategoryServices>();
            builder.Services.AddScoped<ibrandRepository, BrandRepository>();
            builder.Services.AddScoped<ibrandService, brandService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            // autoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //Identity setting
            builder.Services.AddIdentity<MyUser, IdentityRole<Guid>>().AddEntityFrameworkStores<_2B_EgyptDBContext>().AddDefaultTokenProviders();
            builder.Services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme=JwtBearerDefaults.AuthenticationScheme;
                }
                );

            //Add Email Configs
            var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<MailSettings>();
            builder.Services.AddSingleton(emailConfig);
        
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
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
