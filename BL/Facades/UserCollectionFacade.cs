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
    public class UserCollectionFacade
    {
        private IUnitOfWorkProvider uowp;
        private UserService _userService;
        private SpeciesService _speciesService;
        private GenusService _genusService;

        public UserCollectionFacade(IUnitOfWorkProvider unitOfWorkProvider,
            UserService userService,
            SpeciesService speciesService,
            GenusService genusService)
        {
            uowp = unitOfWorkProvider;
            _userService = userService;
            _speciesService = speciesService;
            _genusService = genusService;
        }

        public async Task<List<UserInfoDto>> GetAllUserWithNameAsync(string name)
        {
            using (var uow = uowp.Create())
            {
                return (List<UserInfoDto>) await _userService.GetUsersWithNameAsync(name);
            }
        }

        public async Task<List<SpeciesDto>> GetAllApprovedSpeciesWithGenus(GenusDto genus)
        {
            using (var uow = uowp.Create())
            {
                return (List<SpeciesDto>) await _speciesService.getAllApprovedSpeciesWithGenus(genus);
            }
        }

        public List<GenusDto> GetAllGenuses()
        {
            using (var uow = uowp.Create())
            {
                return (List<GenusDto>) _genusService.GetAllGenuses();
            }
        }

    }
}
