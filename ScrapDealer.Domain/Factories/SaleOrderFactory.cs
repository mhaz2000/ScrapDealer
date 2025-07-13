using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.ValueObjects.SaleOrders;

namespace ScrapDealer.Domain.Factories
{
    public class SaleOrderFactory : ISaleOrderFactory
    {
        public SaleOrder Create(SubCategory? subCategory, Seller seller, SaleOrderAddress address,
            SaleOrderDescription? description, SaleType type, ICollection<Guid> images)
        {
            var addressValue = SaleOrderAddress.Create(address);
            var descriptionValue = SaleOrderDescription.Create(address);

            return new SaleOrder(subCategory, seller, type, descriptionValue, addressValue, images);
        }

        public SaleOrder Update(SubCategory? subCategory, SaleOrderAddress address, SaleOrderDescription? description,
            SaleType type, ICollection<Guid> images, SaleOrder saleOrder)
        {
            var addressValue = SaleOrderAddress.Create(address);
            var descriptionValue = SaleOrderDescription.Create(address);

            saleOrder.Update(subCategory, type, descriptionValue, addressValue, images);

            return saleOrder;
        }
    }
}
