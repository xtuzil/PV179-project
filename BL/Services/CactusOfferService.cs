using AutoMapper;
using BL.DTOs.Offer;
using BL.Services.Interfaces;
using CactusDAL.Models;
using Infrastructure;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class CactusOfferService : ICactusOfferService
    {

        private IMapper mapper;
        private IUnitOfWorkProvider provider;
        private IRepository<CactusOffer> repositoryOffer;
        private IRepository<CactusRequest> repositoryRequest;
        private QueryObject<CactusOfferDto, CactusOffer> queryObject;

        public CactusOfferService(IUnitOfWorkProvider provider,
            IMapper mapper,
            IRepository<CactusOffer> repositoryOffer,
            IRepository<CactusRequest> repositoryRequest,
            QueryObject<CactusOfferDto, CactusOffer> queryObject
        )
        {
            this.mapper = mapper;
            this.provider = provider;
            this.repositoryOffer = repositoryOffer;
            this.repositoryRequest = repositoryRequest;
            this.queryObject = queryObject;
        }
        public void AddCactusOffer(CactusOfferCreateDto cactusOfferDto)
        {
            var cactusOffer = mapper.Map<CactusOffer>(cactusOfferDto);
            repositoryOffer.Create(cactusOffer);
        }

        public void AddCactusRequest(CactusOfferCreateDto cactusRequestDto)
        {
            var cactusRequest = mapper.Map<CactusRequest>(cactusRequestDto);
            repositoryRequest.Create(cactusRequest);
        }
    }
}
