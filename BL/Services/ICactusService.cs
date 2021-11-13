using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface ICactusService
    {
        public Task<IEnumerable<CactusDto>> GetAllUserCactuses(UserInfoDto userInfoDto);
        public Task<IEnumerable<CactusDto>> GetUserCactusesForSale(UserInfoDto userInfoDto);

        public void AddCactus(CactusCreateDto cactusDto);
        public void UpdateCactusInformation(CactusDto cactusDto);

        public void RemoveCactus(CactusDto cactusDto);

    }
}
