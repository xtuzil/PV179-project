using AutoMapper;
using BL.DTOs;
using CactusDAL.Models;
using Infrastructure.Query;

namespace BL.Config
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Cactus, CactusDto>().ReverseMap();
            config.CreateMap<Cactus, CactusUpdateDto>().ReverseMap();
            config.CreateMap<Cactus, CactusCreateDto>().ReverseMap();
            config.CreateMap<CactusDto, CactusCreateDto>().ReverseMap();
            config.CreateMap<CactusOffer, CactusOfferDto>().ReverseMap();
            config.CreateMap<CactusOffer, CactusOfferCreateDto>().ReverseMap();
            config.CreateMap<CactusOffer, CactusOfferUpdateDto>().ReverseMap();
            config.CreateMap<CactusRequest, CactusOfferDto>().ReverseMap();
            config.CreateMap<CactusRequest, CactusOfferCreateDto>().ReverseMap();
            config.CreateMap<Genus, GenusDto>().ReverseMap();
            config.CreateMap<Offer, OfferDto>()
                .ForMember(offerDto => offerDto.OfferedCactuses, opt => opt.MapFrom(src => src.CactusOffers))
                .ForMember(offerDto => offerDto.RequestedCactuses, opt => opt.MapFrom(src => src.CactusRequests))
                .ReverseMap();
            config.CreateMap<Offer, OfferCreateDto>().ReverseMap();
            config.CreateMap(typeof(QueryResult<>), typeof(QueryResultDto<>)).ReverseMap();
            //config.CreateMap<QueryResultDto<Cactus>, QueryResultDto<CactusDto>>().ReverseMap();
            //config.CreateMap<QueryResultDto<Offer>, QueryResultDto<OfferDto>>().ReverseMap();
            //config.CreateMap<QueryResultDto<Species>, QueryResultDto<SpeciesDto>>().ReverseMap();
            //config.CreateMap<QueryResultDto<User>, QueryResultDto<UserInfoDto>>().ReverseMap();
            config.CreateMap<Report, ReportDto>().ReverseMap();
            config.CreateMap<Review, ReviewDto>().ReverseMap();
            config.CreateMap<Review, ReviewCreateDto>().ReverseMap();
            config.CreateMap<Species, SpeciesDto>().ReverseMap();
            config.CreateMap<Species, SpeciesCreateDto>().ReverseMap();
            config.CreateMap<Transfer, TransferDto>().ReverseMap();
            config.CreateMap<Transfer, TransferUpdateDto>().ReverseMap();
            config.CreateMap<User, UserCreateDto>().ReverseMap();
            config.CreateMap<User, UserUpdateProfileDto>().ReverseMap();
            config.CreateMap<User, ChangePasswordDto>().ReverseMap();
            config.CreateMap<User, UserInfoDto>().ReverseMap();
            config.CreateMap<UserInfoDto, UserUpdateProfileDto>().ReverseMap();
            config.CreateMap<CactusCreateDto, CactusDto>().ReverseMap();
        }
    }
}
