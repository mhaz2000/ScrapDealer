using ScrapDealer.Domain.Consts;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.SaleOrders
{
    public record UpdateSaleOrderCommand(string Address, Guid? SubCategoryId, string? Description, SaleType Type, ICollection<Guid> images, Guid Id) : ICommand;
}
