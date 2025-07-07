using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Queries.Categories;
using ScrapDealer.Infrastructure.EF.Contexts;
using ScrapDealer.Infrastructure.EF.Models;
using ScrapDealer.Infrastructure.ModuleExtensions;
using ScrapDealer.Shared.Abstractions.Queries;
using ScrapDealer.Shared.Models;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Categories
{
    internal class GetCategoriesHandler : IQueryHandler<GetCategoriesQuery, PaginatedResult<CategoryDto>>
    {
        private readonly DbSet<CategoryReadModel> _categories;
        private readonly IMapper _mapper;

        public GetCategoriesHandler(ReadDbContext context, IMapper mapper)
        {
            _categories = context.Categories;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<CategoryDto>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _categories.Include(c => c.SubCategories).AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Name, $"%{query.Search}%"));

            var categories = dbQuery.AsNoTracking();
            var paginatedResult = await categories.
                ToPaginatedResultAsync<CategoryReadModel, CategoryDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;
        }
    }
}
