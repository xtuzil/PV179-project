using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface ISpeciesService
    {
        public Task<IEnumerable<SpeciesDto>> GetAllAprovedSpecies();

        public Task<IEnumerable<SpeciesDto>> getAllApprovedSpeciesWithGenus(int genusdId);
    }
}
