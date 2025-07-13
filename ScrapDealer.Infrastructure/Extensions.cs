using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using ScrapDealer.Application;
using ScrapDealer.Application.Services;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Infrastructure.Caching;
using ScrapDealer.Infrastructure.EF;
using ScrapDealer.Infrastructure.EF.SeedData;
using ScrapDealer.Infrastructure.Logging;
using ScrapDealer.Infrastructure.Options;
using ScrapDealer.Infrastructure.Profiles;
using ScrapDealer.Infrastructure.Services;

namespace ScrapDealer.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue("Redis:ConnectionString", "localhost:6379,password=123456");
                options.InstanceName = "ScrapDealer";
            });

            services.AddSingleton<IMinioClient>(sp =>
            {
                var settings = configuration.GetSection("Minio").Get<MinioSettings>()!;
                return new MinioClient()
                    .WithEndpoint(settings.Endpoint)
                    .WithCredentials(settings.AccessKey, settings.SecretKey)
                    .WithSSL(settings.UseSSL)
                    .Build();
            });

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IPasswordHasher<object>, PasswordHasher<object>>();
            services.AddSql(configuration);
            services.AddApplication(configuration);

            services.AddAutoMapper(typeof(UserMappingProfile)); 

            services.AddSingleton<LoggingMiddleware>();

            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<ICaptchaService, CaptchaService>();
            services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
            services.AddSingleton<IRedisCacheService, RedisCacheService>();
            services.AddSingleton<IFileStorageService, MinioFileStorageService>();

            services.AddTransient<DatabaseSeeder>();

            return services;
        }

    }
}
