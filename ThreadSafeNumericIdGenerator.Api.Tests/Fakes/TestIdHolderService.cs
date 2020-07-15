﻿using System.Threading;
using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.Application.Base;
using ThreadSafeNumericIdGenerator.Common;
using ThreadSafeNumericIdGenerator.DataContract;

namespace ThreadSafeNumericIdGenerator.Api.Tests.Fakes
{
    class TestIdHolderService : IIdHolderService
    {
        private static long currentId = 0;

        public Task<Result> CreateAsync(CreateIdHolderDto createIdHolderDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<long>> NextAsync(string name)
        {
            Interlocked.Increment(ref currentId);
            return Task.FromResult(Result.Success<long>(currentId));
        }
    }
}
