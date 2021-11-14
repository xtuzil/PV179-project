using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IOfferService
    {
        public Task<OfferDto> GetOffer(int offerId);
        public OfferDto CreateOffer(OfferCreateDto offerDto);
        public OfferDto CreateCounterOffer(OfferCreateDto offerDto, int previousOffer);
        public Task<OfferDto> AcceptOffer(int offerId);
        public Task<OfferDto> RejectOffer(int offerId);
        public Task<IEnumerable<OfferDto>> GetAuthoredOffersForUser(int userId);
        public Task<IEnumerable<OfferDto>> GetReceivedOffersForUser(int userId);
    }
}
