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

        public async Task<IEnumerable<SpeciesDto>> GetAllApprovedSpeciesWithGenus(int genusId)
        {
            using (var uow = uowp.Create())
            {
                return await _speciesService.getAllApprovedSpeciesWithGenus(genusId);
            }
        }

        public async Task<IEnumerable<GenusDto>> GetAllGenuses()
        {
            using (var uow = uowp.Create())
            {
                return await _genusService.GetAllGenuses();
            }
        }

        public async Task<QueryResultDto<CactusDto>> GetAllUserCactuses(int userId, int RequestedPageNumber = 0, int PageSize = 10)
        {
            using (var uow = uowp.Create())
            {
                return await _cactusService.GetAllUserCactuses(userId, RequestedPageNumber, PageSize);
            }
        }

        public async Task<IEnumerable<CactusDto>> GetUserCactusesForSale(int userId)
        {
            using (var uow = uowp.Create())
            {
                return await _cactusService.GetUserCactusesForSale(userId);
            }
        }

        public void AddCactusForSale(CactusUpdateDto cactus)
        {
            using (var uow = uowp.Create())
            {
                cactus.ForSale = true;
                _cactusService.UpdateCactusInformation(cactus);
                uow.Commit();
            }
        }
        public async Task AddCactusToCollection(CactusCreateDto cactus)
        {
            using (var uow = uowp.Create())
            {
                await _cactusService.AddCactus(cactus);
                uow.Commit();
            }
        }

        public void UpdateCactusInformation(CactusUpdateDto cactus)
        {
            using (var uow = uowp.Create())
            {
                _cactusService.UpdateCactusInformation(cactus);
                uow.Commit();
            }
        }

        public async Task RemoveCactus(int cactusId)
        {
            using (var uow = uowp.Create())
            {
                //Because of history of transactions we don't want to delete cactus from db
                await _cactusService.RemoveCactusFromUser(cactusId);
                uow.Commit();
            }
        }

        public async Task<CactusDto> GetCactus(int id)
        {
            using (var uow = uowp.Create())
            {
                return await _cactusService.GetCactus(id);
            }
        }
    }
}
