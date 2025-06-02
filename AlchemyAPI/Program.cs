using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Alchemy.Infrastructure;
using Alchemy.Domain.Interfaces;
using Alchemy.Application.Services;
using Alchemy.Infrastructure.Repositories;
using Alchemy.Domain.Repositories;
using Alchemy.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Alchemy.Infrastructure.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Alchemy.Domain;
using System.Text;
using Alchemy.Domain.Models;


namespace AlchemyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AlchemyAPI",
                    Version = "v1"
                });
            });

            builder.Services.AddAutoMapper(typeof(UserMapper));
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<AlchemyDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'Default' not found.")));

            builder.Services.Configure<JwtHandler>(builder.Configuration.GetSection("Jwt"));
            builder.Services.AddSingleton<JwtHandler>();

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
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
                    var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtHandler>();

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true, 
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                        (builder.Configuration["Jwt:SecretKey"]!))
                    };
                });
            
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("ClientPolicy", policy => policy.RequireRole("User"));
            });

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
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AlchemyAPI V1");
                    c.RoutePrefix = "";
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
