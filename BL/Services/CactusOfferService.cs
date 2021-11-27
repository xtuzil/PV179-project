using AutoMapper;
using BL.DTOs;
using BL.Services.Interfaces;
using CactusDAL.Models;
using Infrastructure;
using Infrastructure.UnitOfWork;
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

        public void UpdateCactusOffer(CactusOfferDto cactusOfferDto)
        {
            var cactusOffer = mapper.Map<CactusOffer>(cactusOfferDto);
            repositoryOffer.Update(cactusOffer);
        }

        public void UpdateCactusRequest(CactusOfferDto cactusRequestDto)
        {
            var cactusRequest = mapper.Map<CactusRequest>(cactusRequestDto);
            repositoryRequest.Update(cactusRequest);
        }

        public async Task UpdateCactusOfferCactusAsync(int Id, int cactusId)
        {
            var cactusOffer = await repositoryOffer.GetAsync(Id);
            cactusOffer.CactusId = cactusId;
            repositoryOffer.Update(cactusOffer);
        }

        public async Task UpdateCactusRequestCactusAsync(int Id, int cactusId)
        {
            var cactusRequest = await repositoryRequest.GetAsync(Id);
            cactusRequest.CactusId = cactusId;
            repositoryRequest.Update(cactusRequest);
        }

    }
}
