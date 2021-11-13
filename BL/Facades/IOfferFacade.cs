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
        public OfferDto CreateOffer(OfferCreateDto offer);
        public OfferDto CreateCounterOffer(OfferCreateDto offer, int previousOfferId);
        public void AcceptOffer(int offerId);
        public void RejectOffer(int offerId);
    }
}
