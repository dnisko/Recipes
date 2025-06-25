
using Common.Settings;
using Mappers;
using Serilog;
using Services.Helpers;

namespace Recipes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var appConfig = builder.Configuration.GetSection("AppSettings");
            builder.Services.Configure<AppSettings>(appConfig);
            var appSettings = appConfig.Get<AppSettings>();

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            builder.Services.AddHttpClient();

            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration
                    .MinimumLevel.Information()
                    .WriteTo.Console()
                    .WriteTo.File(
                        path: $"Logs/log-.txt",
                        rollingInterval: RollingInterval.Day,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                        retainedFileCountLimit: 7
                    )
                    .Enrich.FromLogContext();
            });

            if (appSettings != null) builder.Services.RegisterDbContext(appSettings.ConnectionString);
            builder.Services.RegisterRepositories();
            builder.Services.RegisterServices();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwagger();
            builder.Services.AddCustomCors();
            builder.Services.AddJwt(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();
            Console.WriteLine("========================================Adding data=================================");
            app.Services.EnsureSeeded();
            Console.WriteLine("========================================Data Added=================================");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Recipes API v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
