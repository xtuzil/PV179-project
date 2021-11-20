using BL.DTOs.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces
{
    public interface ICactusOfferService
    {
        public void AddCactusOffer(CactusOfferCreateDto cactusOfferDto);

        public void AddCactusRequest(CactusOfferCreateDto cactusRequestDto);
    }
}
