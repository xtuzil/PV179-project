﻿using AutoMapper;
using BL.DTOs;
using BL.DTOs.Offer;
using BL.DTOs.User;
using CactusDAL.Models;
using Infrastructure.Query;

namespace BL.Config
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<User, UserInfoDto>().ReverseMap();
            config.CreateMap<User, UserCreateDto>().ReverseMap();
            config.CreateMap<QueryResult<User>, QueryResultDto<UserInfoDto>>().ReverseMap();
            config.CreateMap<Genus, GenusDto>().ReverseMap();
            config.CreateMap<Species, SpeciesDto>().ReverseMap();
            config.CreateMap<QueryResult<Species>, QueryResultDto<SpeciesDto>>().ReverseMap();
            config.CreateMap<Cactus, CactusDto>().ReverseMap();
            config.CreateMap<Cactus, CactusCreateDto>().ReverseMap();
            config.CreateMap<CactusDto, CactusCreateDto>().ReverseMap();
            config.CreateMap<QueryResult<Cactus>, QueryResultDto<CactusDto>>().ReverseMap();
            config.CreateMap<Offer, OfferCreateDto>().ReverseMap();
            config.CreateMap<Offer, OfferDto>().ReverseMap();
            config.CreateMap<QueryResult<Offer>, QueryResultDto<OfferDto>>().ReverseMap();
            config.CreateMap<Report, ReportDto>().ReverseMap();
            config.CreateMap<Review, ReviewDto>().ReverseMap();
            config.CreateMap<Transfer, TransferDto>().ReverseMap();
            config.CreateMap<User, UserUpdateDto>().ReverseMap();
            config.CreateMap<CactusOffer, CactusOfferDto>().ReverseMap();
            config.CreateMap<CactusRequest, CactusOfferDto>().ReverseMap();
            config.CreateMap<CactusOffer, CactusOfferCreateDto>().ReverseMap();
            config.CreateMap<CactusRequest, CactusOfferCreateDto>().ReverseMap();
            config.CreateMap<UserInfoDto, UserUpdateDto>().ReverseMap();
        }
    }
}
