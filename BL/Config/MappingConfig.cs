using AutoMapper;
using BL.DTOs;
using CactusDAL.Models;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace BL.Config
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<User, UserInfoDto>().ReverseMap();
            config.CreateMap<QueryResult<User>, QueryResultDto<UserInfoDto>>().ReverseMap();
            config.CreateMap<Genus, GenusDto>().ReverseMap();
            config.CreateMap<Species, SpeciesDto>().ReverseMap();
            config.CreateMap<QueryResult<Species>, QueryResultDto<SpeciesDto>>().ReverseMap();
            config.CreateMap<Cactus, CactusDto>().ReverseMap();
            config.CreateMap<Cactus, CactusCreateDto>().ReverseMap();
            config.CreateMap<QueryResult<Cactus>, QueryResultDto<CactusDto>>().ReverseMap();
            config.CreateMap<Offer, OfferCreateDto>().ReverseMap();
            config.CreateMap<Offer, OfferDto>().ReverseMap();
            config.CreateMap<Report, ReportDto>().ReverseMap();
            config.CreateMap<Review, ReviewDto>().ReverseMap();
            config.CreateMap<Transfer, TransferDto>().ReverseMap();
            config.CreateMap<User, UserUpdateDto>().ReverseMap();
        }                                                                                   
    }
}
