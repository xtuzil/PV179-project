using BL.DTOs;
using CactusDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface ICactusService
    {
        public Task<IEnumerable<CactusDto>> GetCactusesOlderThan(int age);
        public Task<IEnumerable<CactusDto>> GetCactusesWithSpecies(int speciesId);
        public Task<IEnumerable<CactusDto>> GetCactusesWithSpecies(IEnumerable<SpeciesDto> speciesList);
        //public Task<IEnumerable<CactusDto>> GetCactusesLike(string name);
        public Task<IEnumerable<CactusDto>> GetAllUserCactuses(UserInfoDto userInfoDto);
        public Task<IEnumerable<CactusDto>> GetUserCactusesForSale(UserInfoDto userInfoDto);

        public Task<CactusDto> GetCactus(int cactusId);

        public void AddCactus(CactusCreateDto cactusDto);
        public void UpdateCactusInformation(CactusDto cactusDto);

        public Task UpdateCactusOwnerAsync(int cactusId, int userId);

        public Task UpdateCactusAmountAsync(int cactusId, int amount);

        public void RemoveCactus(CactusDto cactusDto);
        public void RemoveCactusFromUser(CactusDto cactusDto);
        public Cactus CreateNewCactusInstanceForTransfer(CactusDto cactusDto, int amount);
    }
}
