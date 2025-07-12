using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.SaleOrders;
using ScrapDealer.Shared.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapDealer.Domain.Entities
{
    internal class SaleOrder : AggregateRoot<Guid>
    {
        public SubCategory? subCategory { get; private set; }
        public Seller Seller { get; private set; }
        public SaleOrderAddress Address { get; set; }
        public ICollection<Guid> Images { get; set; }
        public SaleType SaleType { get; private set; }

        public Guid SellerId { get; private set; }
        public Guid? SubCategoryId { get; private set; }
    }
}
