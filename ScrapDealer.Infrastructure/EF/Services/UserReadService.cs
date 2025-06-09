using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.Services;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Users;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Services
{
    internal sealed class UserReadService : IUserReadService
    {
        private readonly DbSet<UserReadModel> _users;
        private readonly IPasswordHasher<object> _passwordHasher;

        public UserReadService(ReadDbContext context, IPasswordHasher<object> passwordHasher)
        {
            _users = context.Users;
            _passwordHasher = passwordHasher;
        }

        public Task<bool> ExistsByUserNameAsync(string username)
            => _users.AnyAsync(u => u.Username == username.ToLower());

        public async Task<Guid?> ValidateUserCredentialByUsernameAsync(string username, string password)
        {
            var user = await _users.FirstOrDefaultAsync(u => u.Username == username);

            return user is not null &&
                _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success
                ? user?.Id : null;

        }

        public async Task<bool> ValidateUserCredentialByUserIdAsync(Guid id, string password)
        {
            var user = await _users.FirstOrDefaultAsync(u => u.Id == id);

            return user is not null &&
                _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success;

        }

        public async Task<Guid?> GetByPhoneAsync(string phone)
            => (await _users.FirstOrDefaultAsync(c => c.Phone == phone).ConfigureAwait(false))?.Id;
    }
}
