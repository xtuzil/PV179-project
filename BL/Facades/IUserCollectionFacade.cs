using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface IUserCollectionFacade
    {
        public Task<List<SpeciesDto>> GetAllApprovedSpeciesWithGenus(GenusDto genus);

        public List<GenusDto> GetAllGenuses();

        public Task<List<CactusDto>> GetAllUserCactuses(UserInfoDto user);

        public  Task<List<CactusDto>> GetUserCactusesForSale(UserInfoDto user);

        public void AddCactusForSale(CactusDto cactus);

        public void AddCactusToCollection(CactusDto cactus);

        public void UpdateCactusInformation(CactusDto cactus);

        public void RemoveCactus(CactusDto cactus);
    }
}
