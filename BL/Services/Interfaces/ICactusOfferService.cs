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
        public Task AddCactusOffer(int offerId, int cactusId, int amount);

        public Task AddCactusRequest(int offerId, int cactusId, int amount);
        public void UpdateCactusOffer(CactusOfferUpdateDto cactusOfferDto);

        public void UpdateCactusRequest(CactusOfferUpdateDto cactusRequestDto);

        public Task UpdateCactusOfferCactusAsync(int Id, int cactusId);


        public Task UpdateCactusRequestCactusAsync(int Id, int cactusId);

    }
}
