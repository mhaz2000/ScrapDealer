using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.SaleOrders;
using ScrapDealer.Shared.Abstractions.Domain;

namespace ScrapDealer.Domain.Entities
{
    public class SaleOrder : AggregateRoot<Guid>
    {
        public SubCategory? SubCategory { get; private set; }
        public Seller Seller { get; private set; }
        public SaleOrderAddress Address { get; set; }
        public SaleOrderDescription? Description { get; set; }
        public ICollection<Guid> Images { get; set; }
        public SaleType SaleType { get; private set; }

        public Guid SellerId { get; private set; }
        public Guid? SubCategoryId { get; private set; }


        public SaleOrder(SubCategory? category, Seller seller, SaleType type, SaleOrderDescription? description,
            SaleOrderAddress address, ICollection<Guid> images)
        {
            SubCategory = category;
            Seller = seller;
            Address = address;
            Description = description;
            Images = images;
            SaleType = type;
            SellerId = seller.Id;
            SubCategoryId = category?.Id;
        }

        internal void Update(SubCategory? category, SaleType type, SaleOrderDescription? description,
            SaleOrderAddress address, ICollection<Guid> images)
        {
            SubCategory = category;
            Address = address;
            Description = description;
            Images = images;
            SaleType = type;
            SubCategoryId = category?.Id;
        }
    }
}
