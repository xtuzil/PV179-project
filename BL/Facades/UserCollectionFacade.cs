using BL.DTOs;
using BL.Services;
using CactusDAL;
using Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class UserCollectionFacade
    {
        private UserService userService;
        private SpeciesService speciesService;

        public async Task<List<UserInfoDto>> GetAllUserWithNameAsync(string name)
        {
            var uowp = new EntityFrameworkUnitOfWorkProvider(() => new CactusDbContext());

            using (var uow = uowp.Create())
            {
                userService = new UserService(uowp);
                return (List<UserInfoDto>) await userService.GetUsersWithNameAsync(name);

            }
        }

        public async Task<List<SpeciesDto>> GetAllApprovedSpeciesWithGenus(GenusDto genus)
        {
            var uowp = new EntityFrameworkUnitOfWorkProvider(() => new CactusDbContext());

            using (var uow = uowp.Create())
            {
                speciesService = new SpeciesService(uowp);
                return (List<SpeciesDto>) await speciesService.getAllApprovedSpeciesWithGenus(genus);

            }
        }


        public List<GenusDto> GetAllGenuses()
        {
            var uowp = new EntityFrameworkUnitOfWorkProvider(() => new CactusDbContext());

            using (var uow = uowp.Create())
            {
                var genusService = new GenusService(uowp);
                return (List<GenusDto>) genusService.GetAllGenuses();

            }
        }

    }
}
