using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Users;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface IUserFactory
    {
        User Create(Username username, Phone phone, string password);
        User Create(Username username, Phone phone);
    }
}
