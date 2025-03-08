using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Alchemy.Infrastructure;
using Alchemy.Domain.Interfaces;
using Alchemy.Application.Services;
using Alchemy.Infrastructure.Repositories;
using Alchemy.Domain.Repositories;
using Alchemy.Domain.Services;
using Microsoft.AspNetCore.Identity;


namespace AlchemyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AlchemyAPI",
                    Version = "v1"
                });
            });
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<AlchemyDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'Default' not found.")));

            //builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<AlchemyDbContext>()
            //    .AddDefaultTokenProviders();

            //builder.Services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            //    options.AddPolicy("ClientPolicy", policy => policy.RequireRole("Client"));
            //});

            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            builder.Services.AddScoped<IServicesService, ServicesService>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

            builder.Services.AddScoped<IMasterService, MasterService>();
            builder.Services.AddScoped<IMasterRepository, MasterRepository>();

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
