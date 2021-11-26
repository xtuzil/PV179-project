﻿using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades
{
    public interface IUserCollectionFacade
    {
        public Task<List<SpeciesDto>> GetAllApprovedSpeciesWithGenus(int genusId);

        public Task<List<GenusDto>> GetAllGenuses();

        public Task<List<CactusDto>> GetAllUserCactuses(UserInfoDto user);

        public Task<List<CactusDto>> GetUserCactusesForSale(UserInfoDto user);

        public void AddCactusForSale(CactusDto cactus);

        public void AddCactusToCollection(CactusCreateDto cactus);

        public void UpdateCactusInformation(CactusDto cactus);

        public void RemoveCactus(CactusDto cactus);

        public Task<CactusDto> GetCactus(int id);
    }
}
