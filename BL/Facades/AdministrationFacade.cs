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
    public class AdministrationFacade : IAdministrationFacade
    {
        private IUnitOfWorkProvider unitOfWorkProvider;
        private ISpeciesService speciesService;
        private IUserService userService;

        public AdministrationFacade(IUnitOfWorkProvider unitOfWorkProvider, ISpeciesService speciesService, IUserService userService)
        {
            this.unitOfWorkProvider = unitOfWorkProvider;
            this.speciesService = speciesService;
            this.userService = userService;
        }

        public async Task ApproveSpecies(int speciesId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                await speciesService.ApproveSpecies(speciesId);
                uow.Commit();
            }
        }

        public async Task RejectSpecies(int speciesId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                await speciesService.RejectSpecies(speciesId);
                uow.Commit();
            }
        }

        public async Task BlockUser(int userId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                await userService.BanUser(userId, true);
                uow.Commit();
            }
        }

        public async Task UnblockUser(int userId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                await userService.BanUser(userId, false);
                uow.Commit();
            }
        }

        public async Task<IEnumerable<SpeciesDto>> GetAllPendingRequestsForNewSpecies()
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return await speciesService.getAllNewSpeciesProposals();
            }
        }

        public async Task MakeAdmin(int userId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                await userService.MakeAdmin(userId);
                uow.Commit();
            }
        }

        //public Task<List<ReportDto>> GetAllReports()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
