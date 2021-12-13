using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface ITransferFacade
    {
        public Task<TransferDto> GetTransferByOfferId(int offerId);
        public Task<List<ReviewDto>> GetTransferReviews(int transferId);
        public Task<bool> ProcessTransfer(int transferId);
        public Task<bool> ApproveDelivery(int transferId, int userId);
    }
}
