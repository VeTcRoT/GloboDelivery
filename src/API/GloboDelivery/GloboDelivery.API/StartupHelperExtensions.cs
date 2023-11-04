using GloboDelivery.API.Middlewares;
using GloboDelivery.Application;
using GloboDelivery.Infrastructure;
using GloboDelivery.Persistence;

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
            builder.Services.AddSwaggerGen();

            return builder.Build();
        }
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseCustomExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.MapControllers();

            return app;
        }
    }
}
