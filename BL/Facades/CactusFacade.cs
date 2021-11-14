using BL.DTOs;
using BL.Services;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class CactusFacade : ICactusFacade
    {
        private IUnitOfWorkProvider unitOfWorkProvider;
        private ICactusService cactusService;

        public CactusFacade(IUnitOfWorkProvider unitOfWorkProvider,
            ICactusService cactusService)
        {
            this.unitOfWorkProvider = unitOfWorkProvider;
            this.cactusService = cactusService;
        }

        public async Task<List<CactusDto>> GetCactusesLike(string name)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return (List<CactusDto>) await cactusService.GetCactusesLike(name);
            }
        }

        public Task<List<CactusDto>> GetCactusesOlderThan(int age)
        {
            throw new NotImplementedException();
        }

        public Task<List<CactusDto>> GetCactusesWithGenus(int genusId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CactusDto>> GetCactusesWithSpescies(int speciesId)
        {
            throw new NotImplementedException();
        }
    }
}
