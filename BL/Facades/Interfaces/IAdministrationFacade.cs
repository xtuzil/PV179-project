using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface IAdministrationFacade
    {
        //public Task<List<ReportDto>> GetAllReports();
        public Task BlockUser(int userId);
        public Task UnblockUser(int userId);
        public Task<IEnumerable<SpeciesDto>> GetAllPendingRequestsForNewSpecies();
        public Task ApproveSpecies(int speciesId);
        public Task RejectSpecies(int speciesId);
    }
}
