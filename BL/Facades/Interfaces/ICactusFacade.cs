using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface ICactusFacade
    {
        //public Task<List<CactusDto>> GetCactusesLike(string name);
        public Task<CactusDto> GetCactus(int id);
        public Task<IEnumerable<CactusDto>> GetCactusesOlderThan(int age);
        public Task<IEnumerable<CactusDto>> GetCactusesWithSpecies(int speciesId);
        public Task<IEnumerable<CactusDto>> GetCactusesWithGenus(int genusId);
        public Task ProposeNewSpecies(SpeciesCreateDto speciesCreateDto);
        public Task<IEnumerable<SpeciesDto>> GetAllSpecies();
        public Task<IEnumerable<SpeciesDto>> GetAllApprovedSpecies();
        public Task<IEnumerable<SpeciesDto>> GetAllPendingSpecies();
        public Task<IEnumerable<SpeciesDto>> GetAllRejectedSpecies();
    }
}
