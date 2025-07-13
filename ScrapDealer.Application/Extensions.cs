using ScrapDealer.Shared;
using Microsoft.Extensions.DependencyInjection;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Factories;
using Microsoft.Extensions.Configuration;

namespace ScrapDealer.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserFactory, UserFactory>();
            services.AddScoped<IBuyerFactory, BuyerFactory>();
            services.AddScoped<ISellerFactory, SellerFactory>();
            services.AddScoped<IRoleFactory, RoleFactory>();
            services.AddScoped<IBuyerFactory, BuyerFactory>();
            services.AddScoped<ISubCategoryFactory, SubCategoryFactory>();
            services.AddScoped<ICategoryFactory, CategoryFactory>();
            services.AddScoped<ISaleOrderFactory, SaleOrderFactory>();

            services.AddShared(configuration);


            return services;
        }
    }
}
