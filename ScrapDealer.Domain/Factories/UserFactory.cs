using Microsoft.AspNetCore.Identity;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Users;

namespace ScrapDealer.Domain.Factories
{
    public class UserFactory : IUserFactory
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserFactory(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public User Create(Username username, Phone phone, string password)
        {
            var usernameValue = Username.Create(username);
            var phoneValue = Phone.Create(phone);

            var user = new User(usernameValue, phoneValue);
            var passwordHash = PasswordHash.Create(password, _passwordHasher);
            user.SetPassword(passwordHash);

            return user;
        }

        public User Create(Username username, Phone phone)
        {
            var usernameValue = Username.Create(username);
            var phoneValue = Phone.Create(phone);

            var user = new User(usernameValue, phoneValue);
            return user;
        }
    }

}
