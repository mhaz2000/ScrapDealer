using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Category;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface ISubCategoryFactory
    {
        SubCategory Create(CategoryName name, SubCategoryPrice price, Category category);
        SubCategory Update(CategoryName name, SubCategoryPrice price, SubCategory category);
    }
}
