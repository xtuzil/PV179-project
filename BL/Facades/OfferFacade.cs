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
    public class OfferFacade : IOfferFacade
    {
        private IUnitOfWorkProvider unitOfWorkProvider;
        private IOfferService offerService;

        public OfferFacade(IUnitOfWorkProvider unitOfWorkProvider,
            IOfferService offerService)
        {
            this.unitOfWorkProvider = unitOfWorkProvider;
            this.offerService = offerService;
        }

        public void AcceptOffer(int offerId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                offerService.AcceptOffer(offerId);
            }
        }

        public OfferDto CreateCounterOffer(OfferCreateDto offer, int previousOfferId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return offerService.CreateCounterOffer(offer, previousOfferId);
            }
        }

        public OfferDto CreateOffer(OfferCreateDto offer)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                return offerService.CreateOffer(offer);
            }
        }

        public void RejectOffer(int offerId)
        {
            using (var uow = unitOfWorkProvider.Create())
            {
                offerService.RejectOffer(offerId);
            }
        }
    }
}
