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

        public async Task<IEnumerable<CactusDto>> GetCactusesOlderThan(int age)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return await cactusService.GetCactusesOlderThan(age);
            }
        }


        public async Task<IEnumerable<CactusDto>> GetCactusesWithGenus(int genusId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                var specieses = await speciesService.getAllApprovedSpeciesWithGenus(genusId);
                return await cactusService.GetCactusesWithSpecies(specieses);
            }
        }

        public async Task<IEnumerable<CactusDto>> GetCactusesWithSpecies(int speciesId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return await cactusService.GetCactusesWithSpecies(speciesId);
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
                return await speciesService.GetAllSpecies();
            }
        }

        public async Task<IEnumerable<SpeciesDto>> GetAllApprovedSpecies()
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return await speciesService.GetAllAprovedSpecies();
            }
        }
        public async Task<IEnumerable<SpeciesDto>> GetAllPendingSpecies()
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return await speciesService.GetAllPendingSpecies();
            }
        }
        public async Task<IEnumerable<SpeciesDto>> GetAllRejectedSpecies()
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return await speciesService.GetAllRejectedSpecies();
            }
        }

        public async Task<SpeciesDto> GetSpecies(int id)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return await speciesService.GetSpecies(id);
            }
        }
    }
}
