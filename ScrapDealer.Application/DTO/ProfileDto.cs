using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Application.DTO
{
    public record ProfileDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string NationalCode { get; set; }
        public required string Email { get; set; }
        public required string Province { get; set; }
        public required string City { get; set; }
        public required string AddressDescription { get; set; }
        public required string PostalCode { get; set; }
        public required string Phone { get; set; }
        public Gender Gender { get; set; }
    }
}
