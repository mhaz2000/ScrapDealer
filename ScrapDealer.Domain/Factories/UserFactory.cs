using Microsoft.AspNetCore.Identity;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Users;

namespace ScrapDealer.Domain.Factories
{
    public class UserFactory : IUserFactory
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private const string defaultPass = "$CR@pDe@ler!!";

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

            var random = new Random();
            var passwordHash = PasswordHash.Create(defaultPass + random.Next(1000000, 9999999), _passwordHasher);

            var user = new User(usernameValue, phoneValue);
            user.SetPassword(passwordHash);
            return user;
        }
    }

}
