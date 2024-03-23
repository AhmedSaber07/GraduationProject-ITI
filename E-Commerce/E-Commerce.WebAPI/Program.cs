
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
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
            builder.Services.AddScoped<IuserService, UserService>();
            builder.Services.AddScoped<iuserRepository, UserRepository>();

            builder.Services.AddCors(op =>
            {
                op.AddPolicy("Default", policy =>
                {

                    policy.AllowAnyHeader().
                           AllowAnyMethod().
                           AllowAnyOrigin();
                });
            });
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
                }).AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                    };
                });

            //Add Email Configs
            var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<MailSettings>();
            builder.Services.AddSingleton(emailConfig);
        
            builder.Services.AddDbContext<_2B_EgyptDBContext>(
             op =>
             {
                 op.UseSqlServer(builder.Configuration.GetConnectionString("db"));
             }
             );
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
        //  if (app.Environment.IsDevelopment())
          //  {
                app.UseSwagger();
                app.UseSwaggerUI();
           //}
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("Default");

            app.MapControllers();

            app.Run();
        }
    }
}
