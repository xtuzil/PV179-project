using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface ICactusFacade
    {
        public Task<List<CactusDto>> GetCactusesLike(string name);
        public Task<List<CactusDto>> GetCactusesOlderThan(int age);
        public Task<List<CactusDto>> GetCactusesWithSpecies(int speciesId);
        public Task<List<CactusDto>> GetCactusesWithGenus(int genusId);
    }
}
