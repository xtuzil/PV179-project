using BL.DTOs;
using BL.Services;
using Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class CactusFacade : ICactusFacade
    {
        private IUnitOfWorkProvider unitOfWorkProvider;
        private ICactusService cactusService;
        private IGenusService genusService;
        private ISpeciesService speciesService;

        public CactusFacade(IUnitOfWorkProvider unitOfWorkProvider,
            ICactusService cactusService, IGenusService genusService,
            ISpeciesService speciesService)
        {
            this.unitOfWorkProvider = unitOfWorkProvider;
            this.cactusService = cactusService;
            this.genusService = genusService;
            this.speciesService = speciesService;
        }

        //public async Task<List<CactusDto>> GetCactusesLike(string name)
        //{
        //    using (var uow = unitOfWorkProvider.Create())
        //    {
        //        return (List<CactusDto>) await cactusService.GetCactusesLike(name);
        //    }
        //}

        public async Task<CactusDto> GetCactus(int id)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return await cactusService.GetCactus(id);
            }
        }

        public async Task<List<CactusDto>> GetCactusesOlderThan(int age)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return (List<CactusDto>)await cactusService.GetCactusesOlderThan(age);
            }
        }


        public async Task<List<CactusDto>> GetCactusesWithGenus(int genusId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                var specieses = await speciesService.getAllApprovedSpeciesWithGenus(genusId);
                return (List<CactusDto>)await cactusService.GetCactusesWithSpecies(specieses);
            }
        }

        public async Task<List<CactusDto>> GetCactusesWithSpecies(int speciesId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return (List<CactusDto>)await cactusService.GetCactusesWithSpecies(speciesId);
            }
        }

        public async Task ProposeNewSpecies(SpeciesCreateDto speciesCreateDto)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                await speciesService.CreateSpecies(speciesCreateDto);
                uow.Commit();
            }
        }

        public async Task<IEnumerable<SpeciesDto>> GetAllSpecies()
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return (List<SpeciesDto>)await speciesService.GetAllSpecies();
            }
        }
    }
}
