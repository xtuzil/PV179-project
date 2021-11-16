using AutoMapper;
using BL.DTOs;
using BL.Services;
using Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class TransferFacade : ITransferFacade
    {
        private IMapper mapper;
        private ITransferService transferService;
        private IReviewService reviewService;
        private ICactusService cactusService;
        private IOfferService offerService;
        private IUnitOfWorkProvider uowp;

        public TransferFacade(IMapper mapper,
            ITransferService transferService,
            IReviewService reviewService,
            ICactusService cactusService,
            IOfferService offerService,
            IUnitOfWorkProvider unitOfWorkProvider
        )
        {
            this.mapper = mapper;
            this.transferService = transferService;
            this.reviewService = reviewService;
            this.cactusService = cactusService;
            this.offerService = offerService;
            uowp = unitOfWorkProvider;
        }

        public async Task<List<ReviewDto>> GetTransferReviews(int transferId)
        {
            using (var uow = uowp.Create())
            {
                return (List<ReviewDto>)await reviewService.GetReviewsOfTransfer(transferId);
            }
        }

        public async void ProcessTransfer(int transferId)
        {

        }
    }
}
