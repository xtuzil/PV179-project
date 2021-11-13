using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface ICactusFacade
    {
        public Task<List<CactusDto>> GetCactusesLike(string name);
        public Task<List<CactusDto>> GetCactusesOlderThan(int age);
        public Task<List<CactusDto>> GetCactusesWithSpescies(int speciesId);
        public Task<List<CactusDto>> GetCatusesWithGenus(int genusId);
    }
}
