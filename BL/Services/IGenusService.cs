using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IGenusService
    {
        public IEnumerable<GenusDto> GetAllGenuses();
        public Task<GenusDto> GetGenusById(int id);
    }
}
