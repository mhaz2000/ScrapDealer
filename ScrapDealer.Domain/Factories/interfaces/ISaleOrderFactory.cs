using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.SaleOrders;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface ISaleOrderFactory
    {
        SaleOrder Create(SubCategory? subCategory, Seller seller, SaleOrderAddress address,
            SaleOrderDescription? description, SaleType type, ICollection<Guid> images);

        SaleOrder Update(SubCategory? subCategory, SaleOrderAddress address,
            SaleOrderDescription? description, SaleType type, ICollection<Guid> images, SaleOrder saleOrder);

    }
}
