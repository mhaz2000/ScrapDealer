using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Category;

namespace ScrapDealer.Domain.Factories
{
    public class CategoryFactory : ICategoryFactory
    {
        public Category Create(string name)
        {
            var nameValue = CategoryName.Create(name);

            return new Category(nameValue);
        }
        public Category Update(string name, Category category)
        {
            var nameValue = CategoryName.Create(name);
            category.Update(nameValue);

            return category;
        }
    }
}
