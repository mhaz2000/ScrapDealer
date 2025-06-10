﻿using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Profiles;

namespace ScrapDealer.Domain.Factories.interfaces
{
    public interface IBuyerFactory
    {
        Buyer Create(string fisrtName, string lastName, NationalCode nationalCode, string city, string province, string postalCode,
            string addressDescription, Email email, Gender gender, Guid userId);

        Buyer Update(string fisrtName, string lastName, NationalCode nationalCode, string city, string province, string postalCode,
            string addressDescription, Email email, Gender gender, Buyer buyer);
    }
}
