using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IGenusService
    {
        public IEnumerable<GenusDto> GetAllGenuses();
    }
}
