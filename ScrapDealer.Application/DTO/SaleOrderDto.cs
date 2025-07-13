namespace ScrapDealer.Application.DTO
{
    public record SaleOrderDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string? Description { get; set; }
        public ICollection<Guid> Images { get; set; }
        public SubCategoryDto? SubCategory { get; set; }
        public string SellerName { get; set; }
    }
}
