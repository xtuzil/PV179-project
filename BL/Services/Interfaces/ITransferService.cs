using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface ITransferService
    {
        public Task<TransferDto> GetTransfer(int transferId);

        public void CreateTransfer(int offerId);

        public Task SetTransferTimeAsync(int transferId);

        public Task ApproveDelivery(int transferId, bool authorApproving);

        public void UpdateTransfer(TransferDto transferDto);
        public Task<TransferDto> GetTransferByOfferId(int offerId);
    }
}
