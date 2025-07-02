using ScrapDealer.Domain.Entities;
using ScrapDealer.Infrastructure.EF.Config.Buyers;
using ScrapDealer.Infrastructure.EF.Config.Users;
using ScrapDealer.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Infrastructure.EF.Config.Categories;

namespace ScrapDealer.Infrastructure.EF.Config
{
    public static class DbContextConfigurationApplier
    {
        public static void ApplyReadConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<UserReadModel>(new UserReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<RoleReadModel>(new UserReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<UserRoleReadModel>(new UserReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<CategoryReadModel>(new CategoryReadEntityConfiguration());
            modelBuilder.ApplyConfiguration<SubCategoryReadModel>(new CategoryReadEntityConfiguration());

            modelBuilder.ApplyConfiguration(new BuyerReadEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SellerReadEntityConfiguration());
        }

        public static void ApplyWriteConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<User>(new UserWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<Role>(new UserWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<UserRole>(new UserWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<Category>(new CategoryWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration<SubCategory>(new CategoryWriteEntityConfiguration());

            modelBuilder.ApplyConfiguration(new BuyerWriteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SellerWriteEntityConfiguration());
        }
    }
}
