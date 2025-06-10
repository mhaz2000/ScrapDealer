using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.Profiles;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class Seller : AggregateRoot<Guid>
    {
        public PersonName PersonName { get; private set; }
        public NationalCode NationalCode { get; private set; }
        public Address Address { get; private set; }
        public Email Email { get; private set; }
        public Gender Gender { get; private set; }
        public PersonType PersonType { get; private set; }
        public User User { get; private set; }
        public Guid UserId { get; private set; }

        public bool Verified { get; private set; }

        public Seller()
        {
            
        }

        public Seller(PersonName personName, NationalCode nationalCode,
            Address address, Email email, PersonType personType, Gender gender, Guid userId)
        {
            PersonName = personName;
            NationalCode = nationalCode;
            Address = address;
            Email = email;
            Gender = gender;
            PersonType = personType;
            UserId = userId;
            Verified = false;
        }

        public void Update(PersonName personName, NationalCode nationalCode,
            Address address, Email email, PersonType personType, Gender gender)
        {
            PersonName = personName;
            NationalCode = nationalCode;
            Address = address;
            Email = email;
            Gender = gender;
            PersonType = personType;
        }

        public void SetAsVerified() => Verified = true;

    }
}
