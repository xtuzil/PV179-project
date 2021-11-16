using BL.DTOs;
using BL.Services;
using CactusDAL;
using Infrastructure.EntityFramework;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<List<SpeciesDto>> GetAllApprovedSpeciesWithGenus(GenusDto genus)
        {
            using (var uow = uowp.Create())
            {
                return (List<SpeciesDto>) await _speciesService.getAllApprovedSpeciesWithGenus(genus.Id);
            }
        }

        public List<GenusDto> GetAllGenuses()
        {
            using (var uow = uowp.Create())
            {
                return (List<GenusDto>) _genusService.GetAllGenuses();
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




    }
}
