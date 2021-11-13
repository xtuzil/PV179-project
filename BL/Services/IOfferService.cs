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
        public OfferDto CreateOffer(OfferCreateDto offerDto);
        public Task<OfferDto> AcceptOffer(int offerId);
        public Task<OfferDto> RejectOffer(int offerId);
        public Task<IEnumerable<OfferDto>> GetAuthoredOffersForUser(int userId);
        public Task<IEnumerable<OfferDto>> GetReceivedOffersForUser(int userId);
    }
}
