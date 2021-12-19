using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface IUserCollectionFacade
    {
        public Task<IEnumerable<SpeciesDto>> GetAllApprovedSpeciesWithGenus(int genusId);

        public Task<IEnumerable<GenusDto>> GetAllGenuses();

        public Task<IEnumerable<CactusDto>> GetAllUserCactuses(int userId, int RequestedPageNumber = 0, int PageSize = 10);

        public Task<IEnumerable<CactusDto>> GetUserCactusesForSale(int userId);

        public void AddCactusForSale(CactusUpdateDto cactus);

        public Task AddCactusToCollection(CactusCreateDto cactus);

        public void UpdateCactusInformation(CactusUpdateDto cactus);

        public Task RemoveCactus(int cactusId);

        public Task<CactusDto> GetCactus(int id);
    }
}
