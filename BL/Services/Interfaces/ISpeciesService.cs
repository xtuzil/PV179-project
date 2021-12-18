using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface ISpeciesService
    {
        public Task<SpeciesDto> GetSpecies(int id);
        public Task<IEnumerable<SpeciesDto>> GetAllSpecies();
        public Task<IEnumerable<SpeciesDto>> GetAllAprovedSpecies();
        public Task<IEnumerable<SpeciesDto>> GetAllPendingSpecies();
        public Task<IEnumerable<SpeciesDto>> GetAllRejectedSpecies();
        public Task<IEnumerable<SpeciesDto>> getAllApprovedSpeciesWithGenus(int genusdId);
        public Task CreateSpecies(SpeciesCreateDto speciesCreateDto);
        public Task ApproveSpecies(int speciesId);
        public Task RejectSpecies(int speciesId);
        public Task DeleteSpecies(int speciesId);
        public Task<IEnumerable<SpeciesDto>> getAllNewSpeciesProposals();
    }
}
