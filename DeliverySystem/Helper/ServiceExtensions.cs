using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helper;
using Domain.Interface;
using Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DeliverySystem.Helper
{
    public static class ServiceExtensions
    {

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("CorsPolicy", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowAnyHeader());
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "JWT Token Authentication API",
                    Description = "ASP.NET Core 5.0 Web API"
                });
                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });
        }



        public static void AppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        }

        public static void DependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IAuthentication, AuthenticationService>();
            services.AddScoped<IRole, RoleService>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IProductsAndServiceInterface, ProductsAndServiceImplementation>();
            services.AddScoped<IOrder, OrderService>();
            services.AddScoped<IReport, ReportService>();
            services.AddScoped<IRoute, RouteService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IDispatch, DispatchService>();
            services.AddScoped<ICustomerService, CustomerService>();

        }
    }


}
