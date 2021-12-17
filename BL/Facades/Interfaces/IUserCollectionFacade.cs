using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface IUserCollectionFacade
    {
        public Task<IEnumerable<SpeciesDto>> GetAllApprovedSpeciesWithGenus(int genusId);

        public Task<IEnumerable<GenusDto>> GetAllGenuses();

        public Task<IEnumerable<CactusDto>> GetAllUserCactuses(int userId);

        public Task<IEnumerable<CactusDto>> GetUserCactusesForSale(int userId);

        public Task AddCactusForSale(CactusUpdateDto cactus);

        public Task AddCactusToCollection(CactusCreateDto cactus);

        public Task UpdateCactusInformation(CactusUpdateDto cactus);

        public Task RemoveCactus(int cactusId);

        public Task<CactusDto> GetCactus(int id);
    }
}
