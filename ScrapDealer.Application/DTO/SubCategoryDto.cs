namespace ScrapDealer.Application.DTO
{
    public record SubCategoryDto(Guid Id, string Name, decimal Price, Guid ParentCategoryId);
}
