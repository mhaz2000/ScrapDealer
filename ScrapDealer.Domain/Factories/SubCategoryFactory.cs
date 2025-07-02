using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.Category;

namespace ScrapDealer.Domain.Factories
{
    public class SubCategoryFactory : ISubCategoryFactory
    {
        public SubCategory Create(CategoryName name, SubCategoryPrice price, Category category)
        {
            var nameValue = CategoryName.Create(name);
            var priceValue = SubCategoryPrice.Create(price);

            return new SubCategory(price, name, category);
        }
    }
}
