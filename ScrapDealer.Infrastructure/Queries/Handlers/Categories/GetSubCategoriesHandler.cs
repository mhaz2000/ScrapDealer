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
    internal class GetSubCategoriesHandler : IQueryHandler<GetSubCategoriesQuery, PaginatedResult<SubCategoryDto>>
    {
        private readonly DbSet<SubCategoryReadModel> _categories;
        private readonly IMapper _mapper;

        public GetSubCategoriesHandler(ReadDbContext context, IMapper mapper)
        {
            _categories = context.SubCategories;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<SubCategoryDto>> Handle(GetSubCategoriesQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _categories.AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
                dbQuery = dbQuery
                    .Where(u => Microsoft.EntityFrameworkCore.EF.Functions.Like(u.Name, $"%{query.Search}%"));

            var categories = dbQuery.AsNoTracking();
            var paginatedResult = await categories.
                ToPaginatedResultAsync<SubCategoryReadModel, SubCategoryDto>(query.PageIndex, query.PageSize, query.SortBy ?? string.Empty, _mapper);

            return paginatedResult;
        }

    }
}
