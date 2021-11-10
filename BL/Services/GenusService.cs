using AutoMapper;
using BL.Config;
using BL.DTOs;
using CactusDAL.Models;
using Infrastructure.EntityFramework;
using Infrastructure.Predicates;
using Infrastructure.Predicates.Operators;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class GenusService : IGenusService
    {
        private IMapper mapper;
        private IUnitOfWorkProvider provider;

        public GenusService(IUnitOfWorkProvider provider)
        {
            mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
            this.provider = provider;
        }

        public IEnumerable<GenusDto> GetAllGenuses()
        {
            var genusRepositary = new EntityFrameworkRepository<Genus>((EntityFrameworkUnitOfWorkProvider)provider);
            var genuses = genusRepositary.GetAll();
            return mapper.Map<IEnumerable<GenusDto>>(genuses);
        }

        /*public GenusDto GetGenusById()
        {
            var genusRepositary = new EntityFrameworkRepository<Genus>((EntityFrameworkUnitOfWorkProvider)provider);
            var genuses = genusRepositary.GetAll();
            return mapper.Map<IEnumerable<GenusDto>>(genuses);
        }*/
    }
}
