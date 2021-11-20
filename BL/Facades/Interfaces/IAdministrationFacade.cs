using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface IAdministrationFacade
    {
        public Task<List<ReportDto>> GetAllReports();
        public void BlockUser(int userId);
        public Task<List<SpeciesDto>> GetAllPendingRequestsForNewSpecies();
        public Task<SpeciesDto> ApproveSpecies(int speciesId);
        public Task<SpeciesDto> RejectSpecies(int speciesId);
    }
}
