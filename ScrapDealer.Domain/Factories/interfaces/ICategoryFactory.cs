using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Category;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface ICategoryFactory
    {
        Category Create(CategoryName name);
    }
}
