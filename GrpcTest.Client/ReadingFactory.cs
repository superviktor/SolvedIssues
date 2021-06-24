using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using GrpcTest.Api.Protos;

namespace GrpcTest.Client
{
    public class ReadingFactory
    {
        public Task<ReadingMessage> Generate(int customerId)
        {
            var reading = new ReadingMessage
            {
                CustomerId = customerId,
                ReadingTime = Timestamp.FromDateTime(DateTime.UtcNow),
                ReadingValue = new Random().Next(10000)
            };

            return Task.FromResult(reading);
        }
    }
}