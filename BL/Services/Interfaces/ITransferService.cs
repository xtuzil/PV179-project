using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface ITransferService
    {
        public Task<TransferDto> GetTransfer(int transferId);
        public Task<IEnumerable<TransferDto>> GetTransfersOfUser(int userId);

        public void CreateTransfer(int offerId);

        public Task SetTransferTimeAsync(int transferId);

        public void UpdateTransfer(TransferDto transferDto);
    }
}
