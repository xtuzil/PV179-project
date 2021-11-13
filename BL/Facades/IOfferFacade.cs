using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface IOfferFacade
    {
        public OfferDto CreateOffer(OfferCreateDto offer, List<CactusOfferCreateDto> offeredCactuses, List<CactusRequestCreateDto> requestedCactuses);
        public OfferDto CreateCounterOffer(OfferCreateDto offer, List<CactusOfferCreateDto> offeredCactuses, List<CactusRequestCreateDto> requestedCactuses, int previousOfferId);
        public void AcceptOffer(int offerId);
        public void RejectOffer(int offerId);
    }
}
