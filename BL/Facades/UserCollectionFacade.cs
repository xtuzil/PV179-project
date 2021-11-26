using BL.DTOs;
using BL.Services;
using Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class UserCollectionFacade : IUserCollectionFacade
    {
        private readonly IUnitOfWorkProvider uowp;
        private readonly ISpeciesService _speciesService;
        private readonly IGenusService _genusService;
        private readonly ICactusService _cactusService;

        public UserCollectionFacade(IUnitOfWorkProvider unitOfWorkProvider,
            ISpeciesService speciesService,
            IGenusService genusService,
            ICactusService cactusService)
        {
            uowp = unitOfWorkProvider;
            _speciesService = speciesService;
            _genusService = genusService;
            _cactusService = cactusService;
        }

        public async Task<List<SpeciesDto>> GetAllApprovedSpeciesWithGenus(int genusId)
        {
            using (var uow = uowp.Create())
            {
                return (List<SpeciesDto>)await _speciesService.getAllApprovedSpeciesWithGenus(genusId);
            }
        }

        public async Task<List<GenusDto>> GetAllGenuses()
        {
            using (var uow = uowp.Create())
            {
                return (List<GenusDto>) await _genusService.GetAllGenuses();
            }
        }

        public async Task<List<CactusDto>> GetAllUserCactuses(UserInfoDto user)
        {
            using (var uow = uowp.Create())
            {
                return (List<CactusDto>)await _cactusService.GetAllUserCactuses(user);
            }
        }

        public async Task<List<CactusDto>> GetUserCactusesForSale(UserInfoDto user)
        {
            using (var uow = uowp.Create())
            {
                return (List<CactusDto>)await _cactusService.GetUserCactusesForSale(user);
            }
        }

        public void AddCactusForSale(CactusDto cactus)
        {
            using (var uow = uowp.Create())
            {
                cactus.ForSale = true;
                _cactusService.UpdateCactusInformation(cactus);
                uow.Commit();
            }
        }
        public void AddCactusToCollection(CactusCreateDto cactus)
        {
            using (var uow = uowp.Create())
            {
                _cactusService.AddCactus(cactus);
                uow.Commit();
            }
        }

        public void UpdateCactusInformation(CactusDto cactus)
        {
            using (var uow = uowp.Create())
            {
                _cactusService.UpdateCactusInformation(cactus);
                uow.Commit();
            }
        }

        public void RemoveCactus(CactusDto cactus)
        {
            using (var uow = uowp.Create())
            {
                _cactusService.RemoveCactus(cactus);
                uow.Commit();
            }
        }

        public async Task<CactusDto> GetCactus(int id)
        {
            return await _cactusService.GetCactus(id);
        }
    }
}
