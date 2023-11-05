using GloboDelivery.API.Middlewares;
using GloboDelivery.Application;
using GloboDelivery.Infrastructure;
using GloboDelivery.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace GloboDelivery.API
{
    internal static class StartupHelperExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddInfrastructureServices();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("https://localhost:7221/connect/token")
                        }
                    }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options =>
               {
                   options.Audience = "SwaggerAPI";
                   options.Authority = "https://localhost:7221";
                   options.RequireHttpsMetadata = false;
               });

            builder.Services.AddAuthorization();

            return builder.Build();
        }
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseCustomExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.OAuthClientId("GloboDelivery.API");
                    options.OAuthClientSecret("GloboDelivery.API.Secret");
                });
            }

            app.UseRouting();

            app.MapControllers();

            return app;
        }
    }
}
