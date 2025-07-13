using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.SaleOrders;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Infrastructure.Queries.Handlers.SaleOrders
{
    internal class GetMySaleOrdersHandler : IQueryHandler<GetMySaleOrdersQuery, PaginatedResult<SaleOrderDto>>
    {
        private readonly DbSet<SaleOrderReadModel> _saleOrders;
        private readonly IMapper _mapper;

        public GetMySaleOrdersHandler(ReadDbContext context, IMapper mapper)
        {
            _saleOrders = context.SaleOrders;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<SaleOrderDto>> Handle(GetMySaleOrdersQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _saleOrders.Include(c => c.Seller).Include(t => t.SubCategory).Where(c=> c.Seller.UserId == query.userId).AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => u.SubCategoryId != null && Microsoft.EntityFrameworkCore.EF.Functions.Like(u.SubCategory.Name, $"%{query.Search}%"));

            var saleOrders = dbQuery.AsNoTracking();
            var paginatedResult = await saleOrders.
                ToPaginatedResultAsync<SaleOrderReadModel, SaleOrderDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;
        }
    }
}
