using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Profiles;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.ValueObjects.Category
{
    public class SubCategoryPrice : ValueObject
    {
        public decimal Value { get; }

        private SubCategoryPrice(decimal value)
        {
            if (value < 0)
                throw new BusinessException("مبلغ نمی‌تواند منفی باش.");

            Value = value;

        }

        public static SubCategoryPrice Create(decimal value)
            => new SubCategoryPrice(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }


        public static implicit operator decimal (SubCategoryPrice value)
            => value.Value;

        public static implicit operator SubCategoryPrice(decimal value)
            => new(value);
    }
}
