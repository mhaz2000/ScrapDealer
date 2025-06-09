using ScrapDealer.Infrastructure.EF.Config;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Contexts
{
    internal sealed class ReadDbContext : DbContext
    {
        public DbSet<UserReadModel> Users { get; set; }
        public DbSet<RoleReadModel> Roles { get; set; }
        public DbSet<BuyerReadModel> Buyers { get; set; }
        public DbSet<SellerReadModel> Sellers { get; set; }
        public DbSet<UserRoleReadModel> UserRoles { get; set; }

        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DbContextConfigurationApplier.ApplyReadConfigurations(modelBuilder);
        }
    }
}
