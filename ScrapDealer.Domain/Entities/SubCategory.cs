using ScrapDealer.Domain.ValueObjects.Category;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class SubCategory : Entity<Guid>
    {
        public SubCategoryPrice Price { get; private set; }
        public CategoryName Name { get; private set; }

        public Category Category { get; private set; }
        public Guid CategoryId { get; private set; }

        public SubCategory(SubCategoryPrice price, CategoryName name, Category category)
        {
            Price = price;
            Name = name;
            Category = category; ;
            CategoryId = category.Id;
        }
    }
}
