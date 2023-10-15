using GloboDelivery.Application;
using GloboDelivery.Persistence;
using GloboDelivery.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace GloboDelivery.API
{
    internal static class StartupHelperExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices();

            builder.Services.AddDbContext<GloboDeliveryDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder.Build();
        }
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
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
