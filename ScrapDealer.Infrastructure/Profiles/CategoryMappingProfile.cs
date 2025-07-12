using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<SubCategoryReadModel, SubCategoryDto>()
                .ConstructUsing(c => new SubCategoryDto(c.Id, c.Name, c.Price, c.CategoryId));

            CreateMap<CategoryReadModel, CategoryDto>()
                .ConstructUsing(c => new CategoryDto(c.Id, c.Name, c.SubCategories.Where(s => !s.IsDeleted)
                .Select(s => new SubCategoryDto(s.Id, s.Name, s.Price, s.CategoryId)).ToList()));
        }
    }
}
