using AutoMapper;
using BL.DTOs;
using CactusDAL.Models;
using Infrastructure;
using Infrastructure.Predicates;
using Infrastructure.Predicates.Operators;
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
            var transfer = await repository.GetAsync(transferId);

            return mapper.Map<TransferDto>(transfer);
        }

        public async Task<TransferDto> GetTransferByOfferId(int offerId)
        { 
 
            IPredicate predicate = new SimplePredicate(nameof(Transfer.OfferId), offerId, ValueComparingOperator.Equal);
            var result = await queryObject.ExecuteQueryAsync(new FilterDto() { Predicate = predicate, SortAscending = true });
            var list = (List<TransferDto>) result.Items;
            return list[0];
        }


        public async Task CreateTransfer(int offerId)
        {
            var transfer = new Transfer
            {
                OfferId = offerId
            };
            await repository.Create(transfer);
        }

        public void UpdateTransfer(TransferDto transferDto)
        {
            var transfer = mapper.Map<Transfer>(transferDto);
            repository.Update(transfer);
        }

        public async Task ApproveDelivery(int transferId, bool authorApproving)
        {
            var transfer = await repository.GetAsync(transferId);
            if (authorApproving)
            {
                transfer.AuthorAprovedDelivery = true;
            } else
            {
                transfer.RecipientAprovedDelivery = true;
            }
            repository.Update(transfer);
        }


        public async Task SetTransferTimeAsync(int transferId)
        {
            var transfer = await repository.GetAsync(transferId);
            transfer.TransferedTime = DateTime.UtcNow;
            repository.Update(transfer);
        }
    }
}
