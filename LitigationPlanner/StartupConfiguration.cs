using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace LitigationPlanner.MVC
{
    public static class StartupConfiguration
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title = "Litigation Web Api", Description = "Litigation Swagger" });

                doc.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                doc.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] { }
                    }
                });
            });
        }

        private static void UseSwaggerCustom(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(endpoint =>
            {
                endpoint.SwaggerEndpoint("/swagger/v1/swagger.json", "Litigation Web Api");
            });
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            Lib.StartupConfiguration.ConfigureServices(services);

            services.AddSwagger();
        }

        public static void Setup(this IApplicationBuilder app)
        {
            app.UseSwaggerCustom();
        }
    }
}
