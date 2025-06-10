﻿using AutoMapper;
using ScrapDealer.Application.DTO;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.Profiles
{
    public class BuyerMappingProfile : Profile
    {
        public BuyerMappingProfile()
        {
            CreateMap<BuyerReadModel, BuyerProfileDto>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(b => b.User.Phone));

        }
    }
}
