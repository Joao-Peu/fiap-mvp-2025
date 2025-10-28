using FIAPCloudGames.Application.Dtos;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace FIAPCloudGames.API.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            // Basic info
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "FIAP Cloud Games API",
                Version = "v1",
                Description = "API para gestão de usuários e aquisição de jogos digitais."
            });

            // XML comments for API assembly
            var apiXml = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            if (File.Exists(apiXml))
                options.IncludeXmlComments(apiXml);

            // XML comments for Application (DTOs) assembly
            var appAssemblyName = typeof(UserDto).Assembly.GetName().Name;
            if (!string.IsNullOrWhiteSpace(appAssemblyName))
            {
                var appXml = Path.Combine(AppContext.BaseDirectory, $"{appAssemblyName}.xml");
                if (File.Exists(appXml))
                    options.IncludeXmlComments(appXml);
            }

            // JWT Bearer security definition
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Insira o token JWT no formato: Bearer {seu_token}",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            options.AddSecurityDefinition("Bearer", securityScheme);

            // Require JWT by default (can still allow anonymous endpoints)
            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    Array.Empty<string>()
                }
            };
            options.AddSecurityRequirement(securityRequirement);
        });

        return services;
    }
}