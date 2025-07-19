using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Alchemy.Infrastructure;
using Alchemy.Domain.Interfaces;
using Alchemy.Application.Services;
using Alchemy.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Alchemy.Domain.Models;
using Alchemy.Infrastructure.Options;
using Scalar.AspNetCore;

namespace AlchemyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.Configure<JwtOptions>(
                builder.Configuration.GetSection(JwtOptions.JwtOptionsKey));
            
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<AlchemyDbContext>(option =>
            option.UseNpgsql(builder.Configuration.GetConnectionString("AlchemyConnectionString") ?? throw new InvalidOperationException("Connection string 'Default' not found.")));
            
            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = true;
            })
     .AddEntityFrameworkStores<AlchemyDbContext>()
     .AddDefaultTokenProviders();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true, 
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                        (builder.Configuration["Jwt:SecretKey"]!)),
                        ClockSkew = TimeSpan.Zero // removing the time error for instant expiration of a token
                    };
                });
            
            builder.Services.AddAuthorization();

            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            builder.Services.AddScoped<IServicesService, ServicesService>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

            builder.Services.AddScoped<IMasterService, MasterService>();
            builder.Services.AddScoped<IMasterRepository, MasterRepository>();

            builder.Services.AddScoped<IMasterScheduleService, MasterScheduleService>();
            builder.Services.AddScoped<IMasterScheduleRepository,  MasterScheduleRepository>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("JWT Authentication API");
                });
                app.MapGet("/", context =>
                {
                    context.Response.Redirect("/scalar/", permanent: false);
                    return Task.CompletedTask;
                });
            }
            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
