using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Profiles;

namespace ScrapDealer.Domain.Factories
{
    public class BuyerFactory : IBuyerFactory
    {
        public Buyer Create(string fisrtName, string lastName, NationalCode nationalCode, string city, string province,
            string postalCode, string addressDescription, Email email, PersonType personType, Gender gender, User user)
        {
            var personNameValue = PersonName.Create(fisrtName, lastName);
            var nationalCodeValue = NationalCode.Create(nationalCode);
            var addressValue = Address.Create(province, city, postalCode, addressDescription);
            var emailValue = Email.Create(email);

            return new(personNameValue, nationalCodeValue, addressValue, emailValue, personType, gender, user);
        }

        public Buyer Update(string fisrtName, string lastName, NationalCode nationalCode, string city, string province,
            string postalCode, string addressDescription, Email email, PersonType personType, Gender gender, Buyer buyer)
        {
            var personNameValue = PersonName.Create(fisrtName, lastName);
            var nationalCodeValue = NationalCode.Create(nationalCode);
            var addressValue = Address.Create(province, city, postalCode, addressDescription);
            var emailValue = Email.Create(email);

            buyer.Update(personNameValue, nationalCodeValue, addressValue, email, personType, gender);

            return buyer;
        }
    }
}
