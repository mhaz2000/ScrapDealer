using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.Profiles;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class Buyer : AggregateRoot<Guid>
    {
        public PersonName PersonName { get; private set; }
        public NationalCode NationalCode { get; private set; }
        public ProfileAddress Address { get; private set; }
        public Email Email { get; private set; }
        public Gender Gender { get; private set; }

        public User User { get; private set; }
        public Guid UserId { get; private set; }
        public bool Verified { get; private set; }


        public Buyer()
        {
            
        }

        public Buyer(PersonName personName, NationalCode nationalCode,
            ProfileAddress address, Email email, Gender gender, Guid userId)
        {
            PersonName = personName;
            NationalCode = nationalCode;
            Address = address;
            Email = email;
            Gender = gender;
            UserId = userId;
            Verified = false;
        }

        public void Update(PersonName personName, NationalCode nationalCode,
            ProfileAddress address, Email email, Gender gender)
        {
            PersonName = personName;
            NationalCode = nationalCode;
            Address = address;
            Email = email;
            Gender = gender;
        }

        public void SetAsVerified() => Verified = true;
    }
}
