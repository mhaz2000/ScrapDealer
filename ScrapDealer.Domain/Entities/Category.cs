using ScrapDealer.Domain.ValueObjects.Category;
using ScrapDealer.Domain.ValueObjects.Users;
using ScrapDealer.Shared.Abstractions.Domain;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Domain.Entities
{
    public class Category : AggregateRoot<Guid>
    {
        public CategoryName Name { get; private set; }

        private readonly List<SubCategory> _subCategories = new List<SubCategory>();
        public IReadOnlyCollection<SubCategory> SubCategories => _subCategories.AsReadOnly();

        public Category()
        {
            
        }
        public void AddSubCategory(SubCategory category)
        {
            if (category == null)
                throw new BusinessException("دسته بندی یافت نشد.");

            if (_subCategories.Any(r => r.CategoryId == category.Id))
                throw new BusinessException($"این نوع قبلا در دسته یندی اضافه شده است.");

            _subCategories.Add(category);
        }

        public Category(CategoryName name)
        {
            Name = name;
        }

        internal void Update(CategoryName name)
        {
            Name = name;
        }
    }
}
