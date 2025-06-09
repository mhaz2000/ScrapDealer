using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Profiles;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface IBuyerFactory
    {
        Buyer Create(string fisrtName, string lastName, NationalCode nationalCode, string city, string province, string postalCode,
            string addressDescription, Email email, PersonType personType, Gender gender, User user);

        Buyer Update(string fisrtName, string lastName, NationalCode nationalCode, string city, string province, string postalCode,
            string addressDescription, Email email, PersonType personType, Gender gender, Buyer buyer);
    }
}
