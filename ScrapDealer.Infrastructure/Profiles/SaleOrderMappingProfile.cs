using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles
{
    internal class SaleOrderMappingProfile : Profile
    {
        public SaleOrderMappingProfile()
        {
            CreateMap<SaleOrderReadModel, SaleOrderDto>()
                .ConstructUsing(c => new SaleOrderDto()
                {
                    Id = c.Id,
                    SubCategory = c.SubCategory == null ? null : new SubCategoryDto(c.SubCategory.Id, c.SubCategory.Name, c.SubCategory.Price, c.SubCategory.CategoryId),
                    Address = c.Address,
                    Description = c.Description,
                    Images = c.Images,
                    SellerName = c.Seller.FirstName + " " + c.Seller.LastName
                });
        }
    }
}
