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

            var subCategory = new SubCategory(price, name, category);
            category.AddSubCategory(subCategory);
            return subCategory;
        }

        public SubCategory Update(CategoryName name, SubCategoryPrice price, SubCategory subCategory)
        {
            var nameValue = CategoryName.Create(name);
            var priceValue = SubCategoryPrice.Create(price);

            subCategory.Update(priceValue, nameValue);
            return subCategory;
        }
    }
}
