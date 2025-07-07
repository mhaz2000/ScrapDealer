using ScrapDealer.Domain.Entities;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface ICategoryFactory
    {
        Category Create(string name);
        Category Update(string name, Category category);
    }
}
