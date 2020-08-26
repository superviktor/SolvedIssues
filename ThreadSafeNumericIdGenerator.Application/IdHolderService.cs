using System;
using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.Application.Base;
using ThreadSafeNumericIdGenerator.Common;
using ThreadSafeNumericIdGenerator.DataContract;
using ThreadSafeNumericIdGenerator.Domain.Model.Entities;
using ThreadSafeNumericIdGenerator.Domain.Model.ValueObjects;
using ThreadSafeNumericIdGenerator.Domain.Repository;
using ThreadSafeNumericIdGenerator.Repository.DataContract;

namespace ThreadSafeNumericIdGenerator.Application
{
    public class IdHolderService : IIdHolderService
    {
        private const int StartFromDefault = 1;
        private readonly IIdHolderRepository idHolderRepository;

        public IdHolderService(IIdHolderRepository idHolderRepository)
        {
            this.idHolderRepository = idHolderRepository;
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await idHolderRepository.ExistsAsync(name);
        }

        public async Task CreateAsync(CreateIdHolderDto createIdHolderDto)
        {
            var exists = await ExistsAsync(createIdHolderDto.Name);
            if (exists)
                throw new ArgumentException($"IdHolder Name { createIdHolderDto.Name } is in use");

            var idHolderName = IdHolderName.Create(createIdHolderDto.Name);
            var idHolderCurrentId = IdHolderCurrentId.Create(createIdHolderDto.StartFrom);
            var result = Result.Combine(idHolderName, idHolderCurrentId);
            if (result.IsSuccess)
            {
                var idHolder = new IdHolder(idHolderName.Value, idHolderCurrentId.Value);
                await CreateIdHolderTableEntityAsync(idHolder);
            }
            else
            {
                throw new Exception(result.Error);
            }
        }

        public async Task<long> NextAsync(string name)
        {
            if (await idHolderRepository.ExistsAsync(name))
            {
                var entity = await idHolderRepository.GetAsync(name);
                var idHolderName = IdHolderName.Create(entity.Name);
                var idHolderCurrentId = IdHolderCurrentId.Create(entity.CurrentId);
                var idHolder = new IdHolder(idHolderName.Value, idHolderCurrentId.Value);
                entity.CurrentId = idHolder.Next();
                await idHolderRepository.UpdateAsync(entity);

                return idHolder.CurrentId.Value;
            }
            else
            {
                var idHolderName = IdHolderName.Create(name);
                var idHolderCurrentId = IdHolderCurrentId.Create(StartFromDefault);
                var result = Result.Combine(idHolderName, idHolderCurrentId);
                if (result.IsSuccess)
                {
                    var idHolder = new IdHolder(idHolderName.Value, idHolderCurrentId.Value);
                    await CreateIdHolderTableEntityAsync(idHolder);

                    return StartFromDefault;
                }
                else
                {
                    throw new Exception(result.Error);
                }           
            }
        }

        private async Task CreateIdHolderTableEntityAsync(IdHolder idHolder)
        {
            var entity = new IdHolderTableEntity
            {
                Name = idHolder.Name,
                CurrentId = idHolder.CurrentId
            };

            await idHolderRepository.CreateAsync(entity);
        }
    }
}
