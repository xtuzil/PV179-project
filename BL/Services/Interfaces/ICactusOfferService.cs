﻿using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces
{
    public interface ICactusOfferService
    {
        public void AddCactusOffer(int offerId, int cactusId, int amount);

        public void AddCactusRequest(int offerId, int cactusId, int amount);
        public void UpdateCactusOffer(CactusOfferDto cactusOfferDto);

        public void UpdateCactusRequest(CactusOfferDto cactusRequestDto);

        public Task UpdateCactusOfferCactusAsync(int Id, int cactusId);


        public Task UpdateCactusRequestCactusAsync(int Id, int cactusId);

    }
}
