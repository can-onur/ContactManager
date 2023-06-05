using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Gateway.Configuration
{
    internal static class OcelotExtensions
    {
        public static WebApplication ConfigureOcelot(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors();

            builder.Configuration.AddOcelotWithSwaggerSupport((o) =>
            {
                o.Folder = "Configuration/Ocelot";
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddOcelot();

            builder.Services.AddSwaggerForOcelot(builder.Configuration);

            return builder.Build();
        }


        public static WebApplication ConfigureOcelotPipeline(this WebApplication app)
        {
            app.UseStaticFiles();

            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
            });

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseOcelot().Wait();

            return app;
        }
    }
}
