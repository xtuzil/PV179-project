using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface ITransferFacade
    {
        public Task<List<ReviewDto>> GetTransferReviews(int transferId);
        public void ProcessTransfer(int transferId);
    }
}
