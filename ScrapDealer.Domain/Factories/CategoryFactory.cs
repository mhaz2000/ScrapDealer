using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Category;

namespace ScrapDealer.Domain.Factories
{
    public class CategoryFactory : ICategoryFactory
    {
        public Category Create(CategoryName name)
        {
            var nameValue = CategoryName.Create(name);

            return new Category(nameValue);
        }
    }
}
