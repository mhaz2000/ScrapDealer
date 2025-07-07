namespace ScrapDealer.Application.DTO
{
    public record CategoryDto(Guid Id, string Name, ICollection<SubCategoryDto> SubCategories);
}
