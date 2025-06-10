using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Infrastructure.EF.Contexts;

namespace ScrapDealer.Infrastructure.EF.SeedData
{
    internal class DatabaseSeeder
    {
        private readonly WriteDbContext _context;
        private readonly IUserFactory _userFactory;

        public DatabaseSeeder(WriteDbContext context, IUserFactory userFactory)
        {
            _context = context;
            _userFactory = userFactory;
        }

        public async Task SeedAsync()
        {
            if (!_context.Users.Any() && !_context.Roles.Any())
            {
                var adminRole = new Role(Guid.NewGuid(), "Admin");
                var supportRole = new Role(Guid.NewGuid(), "Support");
                var sellerRole = new Role(Guid.NewGuid(), "Seller");
                var buyerRole = new Role(Guid.NewGuid(), "Buyer");

                var admin = _userFactory.Create("admin", "09100000000", "admin123");
                admin.AddRole(adminRole);

                _context.Roles.AddRange(adminRole, sellerRole, buyerRole, supportRole);
                _context.Users.Add(admin);

                await _context.SaveChangesAsync();
            }
        }
    }
}
