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
            config.CreateMap<QueryResult<Cactus>, QueryResultDto<CactusDto>>().ReverseMap();
        }
    }
}
