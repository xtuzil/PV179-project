using AutoMapper;
using BL.DTOs;
using CactusDAL.Models;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public class TransferService : ITransferService
    {
        private IMapper mapper;
        private IRepository<Transfer> repository;
        private QueryObject<TransferDto, Transfer> queryObject;

        public TransferService(IMapper mapper, IRepository<Transfer> repository,
            QueryObject<TransferDto, Transfer> queryObject)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.queryObject = queryObject;
        }

        public async Task<TransferDto> GetTransfer(int transferId)
        {
            var transfer = await repository.GetAsync(transferId, transfer => transfer.Offer);

            return mapper.Map<TransferDto>(transfer);
        }

        public async Task<IEnumerable<TransferDto>> GetTransfersOfUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
