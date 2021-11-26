using AutoMapper;
using BL.DTOs;
using CactusDAL.Models;
using Infrastructure;
using Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public class GenusService : IGenusService
    {
        private IMapper mapper;
        private IUnitOfWorkProvider provider;
        private IRepository<Genus> repository;

        public GenusService(IUnitOfWorkProvider provider, IMapper mapper, IRepository<Genus> repository)
        {
            this.mapper = mapper;
            this.provider = provider;
            this.repository = repository;
        }

        public async Task<IEnumerable<GenusDto>> GetAllGenuses()
        {
            var genuses = await repository.GetAll();
            return mapper.Map<IEnumerable<GenusDto>>(genuses);
        }

        public async Task<GenusDto> GetGenusById(int id)
        {
            var genus = await repository.GetAsync(id);
            return mapper.Map<GenusDto>(genus);
        }
    }
}
