using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface ISpeciesService
    {
        public Task<IEnumerable<SpeciesDto>> GetAllAprovedSpecies();

        public Task<IEnumerable<SpeciesDto>> getAllApprovedSpeciesWithGenus(int genusdId);
    }
}
