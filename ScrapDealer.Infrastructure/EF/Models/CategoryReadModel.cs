namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class CategoryReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public ICollection<SubCategoryReadModel> SubCategories { get; set; }
    }
}
