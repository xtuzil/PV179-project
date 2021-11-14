using AutoMapper;
using BL.DTOs;
using CactusDAL.Models;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class TransferService : ITransferService
    {
        private IMapper mapper;
        private IRepository<Transfer> repository;

        public TransferService(IMapper mapper, IRepository<Transfer> repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<TransferDto> GetTransfer(int transferId)
        {
            var transfer = await repository.GetAsync(transferId, transfer => transfer.Offer);

            return mapper.Map<TransferDto>(transfer);
        }
    }
}
