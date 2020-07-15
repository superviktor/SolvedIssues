using System;
using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.Application.Base;
using ThreadSafeNumericIdGenerator.DataContract;
using ThreadSafeNumericIdGenerator.Domain.Model.Entities;
using ThreadSafeNumericIdGenerator.Domain.Repository;

namespace ThreadSafeNumericIdGenerator.Application
{
    public class IdHolderService : IIdHolderService
    {
        private readonly IIdHolderRepository idHolderRepository;

        public IdHolderService(IIdHolderRepository idHolderRepository)
        {
            this.idHolderRepository = idHolderRepository;
        }

        public async Task CreateAsync(CreateIdHolderDto createIdHolderDto)
        {
            if (await idHolderRepository.ExistsAsync(createIdHolderDto.Name))
                throw new Exception($"IdHolder Name { createIdHolderDto.Name } is in use");

            var idHolderCreateResult = IdHolder.Create(createIdHolderDto.Name, createIdHolderDto.StartFrom);
            if (idHolderCreateResult.IsSuccess)
            {
                await idHolderRepository.CreateAsync(idHolderCreateResult.Value);
            }
            else
            {
                //is this best way ? maybe some result ?
                throw new Exception(idHolderCreateResult.Error);
            }

        }

        public Task<bool> ExistsAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<long> NextAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
