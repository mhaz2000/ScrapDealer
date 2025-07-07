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
            .ForMember(dest => dest.ParentCategoryId, opt =>
                opt.MapFrom(src => src.Category.Id));

            CreateMap<CategoryReadModel, CategoryDto>()
                .ForMember(dest => dest.SubCategories, opt =>
                    opt.MapFrom(src => src.SubCategories.Where(sc => !sc.IsDeleted)));
        }
    }
}
