namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class SubCategoryReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryReadModel Category { get; set; }
    }
}
