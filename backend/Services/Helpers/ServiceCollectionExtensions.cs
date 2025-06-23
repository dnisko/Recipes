using System.Text;
using Common.Settings;
using DataAccess;
using DataAccess.Implementations;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Implementations;
using Services.Interfaces;

namespace Services.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RecipeDbContext>(options =>
                options.UseSqlServer(connectionString)
                    //.EnableSensitiveDataLogging() // This enables detailed logging of SQL queries
                    .LogTo(Console.WriteLine, LogLevel.Information)); //,
            //ServiceLifetime.Scoped);

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                //c.TagActionsBy(api => new[] { api.GroupName ?? "Uncategorized" });
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Recipes API",
                    Version = "v1",
                    Description = "API for managing recipes and related data",
                });
                //c.OpenApiGeneratorOptions = new Swashbuckle.AspNetCore.SwaggerGen.OpenApiGeneratorOptions
                //{
                //    OpenApiVersion = "3.0.1"
                //};
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer {token}')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            var token = appSettings.Secret;
            services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            return services;
        }
    }
}
