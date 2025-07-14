using ScrapDealer.Application.DTO;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Application.Queries.Buyers
{
    public record GetBuyersQuery(bool Verified) : PaginationQuery, IQuery<PaginatedResult<BuyerProfileDto>>;

}
