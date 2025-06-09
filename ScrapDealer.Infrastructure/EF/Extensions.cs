using ScrapDealer.Application.Services;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Options;
using ScrapDealer.Infrastructure.EF.Services;
using ScrapDealer.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScrapDealer.Infrastructure.EF.Repositories.Base;
using ScrapDealer.Domain.Repositories.Base;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Infrastructure.EF
{
    internal static class Extensions
    {
        public static IServiceCollection AddSql(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<DbInitializer>();

            var options = configuration.GetOptions<SqlOptions>("Sql");

            services.AddDbContext<ReadDbContext>(ctx => ctx.UseSqlServer(options.ConnectionString));
            services.AddDbContext<WriteDbContext>(ctx => ctx.UseSqlServer(options.ConnectionString));

            services.AddScoped<IUserReadService, UserReadService>();
            services.AddScoped<IRoleReadService, RoleReadService>();


            services.Scan(scan => scan
           .FromAssemblyOf<GenericRepository<Entity<Guid>>>() 
           .AddClasses(classes => classes.AssignableTo(typeof(IGenericRepository<>)))
               .AsImplementedInterfaces()
               .WithScopedLifetime()
           .AddClasses(classes => classes.AssignableTo(typeof(GenericRepository<>)))
               .AsImplementedInterfaces()
               .WithScopedLifetime());


            return services;
        }
    }
}
