﻿using ScrapDealer.Domain.Consts;

namespace ScrapDealer.Infrastructure.EF.Models
{
    internal class BuyerReadModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string NationalCode { get; set; }
        public required string Province { get; set; }
        public required string City { get; set; }
        public required string PostalCode { get; set; }
        public required string AddressDescription { get; set; }
        public required string Email { get; set; }
        public PersonType PersonType { get; set; }
        public Gender Gender { get; set; }

        public required UserReadModel User { get; set; }
        public Guid UserId { get; set; }
    }
}
