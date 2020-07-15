using System;
using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.Application.Base;
using ThreadSafeNumericIdGenerator.Common;
using ThreadSafeNumericIdGenerator.DataContract;
using ThreadSafeNumericIdGenerator.Domain.Model.Entities;
using ThreadSafeNumericIdGenerator.Domain.Repository;
using ThreadSafeNumericIdGenerator.Repository.DataContract;

namespace ThreadSafeNumericIdGenerator.Application
{
    public class IdHolderService : IIdHolderService
    {
        private readonly IIdHolderRepository idHolderRepository;

        public IdHolderService(IIdHolderRepository idHolderRepository)
        {
            this.idHolderRepository = idHolderRepository;
        }

        public async Task<Result> CreateAsync(CreateIdHolderDto createIdHolderDto)
        {
            if (await idHolderRepository.ExistsAsync(createIdHolderDto.Name))
                return Result.Fail($"IdHolder Name { createIdHolderDto.Name } is in use");

            var idHolderCreateResult = IdHolder.Create(createIdHolderDto.Name, createIdHolderDto.StartFrom);
            if (idHolderCreateResult.IsSuccess)
            {
                var entity = new IdHolderTableEntity
                {
                    Name = idHolderCreateResult.Value.Name,
                    CurrentId = idHolderCreateResult.Value.CurrentId
                };
                await idHolderRepository.CreateAsync(entity);

                return Result.Success();
            }
            else
            {
                return Result.Fail(idHolderCreateResult.Error);
            }

        }

        public async Task<Result<long>> NextAsync(string name)
        {
            if (await idHolderRepository.ExistsAsync(name))
            {
                var entity = await idHolderRepository.GetAsync(name);
                var idHolder = IdHolder.Create(entity.Name, entity.CurrentId).Value;
                long nextId = idHolder.Next();
                entity.CurrentId = nextId;
                await idHolderRepository.UpdateAsync(entity);

                return Result.Success(nextId);
            }
            else
            {
                // create new holder with default id value
                throw new NotImplementedException();
            }
        }
    }
}
