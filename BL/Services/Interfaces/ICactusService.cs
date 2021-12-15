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
        public Task<IEnumerable<CactusDto>> GetAllUserCactuses(int userId);
        public Task<IEnumerable<CactusDto>> GetUserCactusesForSale(int userId);

        public Task<CactusDto> GetCactus(int id);

        public Task AddCactus(CactusCreateDto cactusDto);
        public void UpdateCactusInformation(CactusUpdateDto cactusDto);

        public Task UpdateCactusOwnerAsync(int cactusId, int userId);

        public Task UpdateCactusAmountAsync(int cactusId, int amount);

        public Task RemoveCactus(int cactusId);
        public Task RemoveCactusFromUser(int cactusId);
        public Task<CactusDto> CreateNewCactusInstanceForTransfer(CactusDto cactusCreateDto, int amount);
    }
}
