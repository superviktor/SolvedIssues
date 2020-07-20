﻿using System;
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

            var idHolderCreateResult = IdHolder.Create(createIdHolderDto.Name, createIdHolderDto.StartFrom);
            if (idHolderCreateResult.IsSuccess)
            {
                await CreateIdHolderTableEntityAsync(idHolderCreateResult);
            }
            else
            {
                throw new Exception(idHolderCreateResult.Error);
            }
        }

        public async Task<long> NextAsync(string name)
        {
            if (await idHolderRepository.ExistsAsync(name))
            {
                var entity = await idHolderRepository.GetAsync(name);
                var idHolder = IdHolder.Create(entity.Name, entity.CurrentId).Value;
                long nextId = idHolder.Next();
                entity.CurrentId = nextId;
                await idHolderRepository.UpdateAsync(entity);

                return nextId;
            }
            else
            {
                var idHolderCreateResult = IdHolder.Create(name, StartFromDefault);
                if (idHolderCreateResult.IsSuccess)
                {
                    await CreateIdHolderTableEntityAsync(idHolderCreateResult);

                    return StartFromDefault;
                }
                else
                {
                    throw new Exception(idHolderCreateResult.Error);
                }
            }
        }

        private async Task CreateIdHolderTableEntityAsync(Result<IdHolder> idHolderCreateResult)
        {
            var entity = new IdHolderTableEntity
            {
                Name = idHolderCreateResult.Value.Name,
                CurrentId = idHolderCreateResult.Value.CurrentId
            };
            await idHolderRepository.CreateAsync(entity);
        }
    }
}
