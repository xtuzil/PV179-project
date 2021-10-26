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
    public class CactusFacade
    {
        private UserService userService;

        public async Task<List<UserInfoDto>> GetAllUserWithNameAsync(string name)
        {
            var uowp = new EntityFrameworkUnitOfWorkProvider(() => new CactusDbContext());

            using (var uow = uowp.Create())
            {
                userService = new UserService(uowp);
                return (List<UserInfoDto>) await userService.GetUsersWithNameAsync(name);

            }
        }
    }
}
