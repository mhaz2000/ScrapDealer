using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class SaleOrderReadModel
    {
        public Guid Id { get; set; }
        public SubCategoryReadModel? SubCategory { get; set; }
        public SellerReadModel Seller { get; set; }
        public required string Address { get; set; }
        public string? Description { get; set; }
        public ICollection<Guid> Images { get; set; }
        public SaleType SaleType { get; set; }

        public Guid SellerId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
