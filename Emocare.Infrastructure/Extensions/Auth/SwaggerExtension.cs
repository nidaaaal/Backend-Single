using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;


namespace Emocare.Infrastructure.Extensions.Auth
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Emocare API",
                    Version = "v1"
                });

                // Define a custom security scheme (raw token only)
                c.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
                {
                    Description = "Enter JWT token ",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "JWT"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}
