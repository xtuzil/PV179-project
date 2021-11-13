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
        public OfferDto CreateOffer(OfferCreateDto offer, List<CactusDto> offeredCactuses, List<CactusDto> requestedCactuses);
        public OfferDto CreateCounterOffer(OfferCreateDto offer, List<CactusDto> offeredCactuses, List<CactusDto> requestedCactuses, int previousOfferId);
        public void AcceptOffer(int offerId);
        public void RejectOffer(int offerId);
    }
}
