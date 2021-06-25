using System;
using System.Text.Json;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcTest.Api.Protos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace GrpcTest.Api.Service
{
    //[Authorize]
    public class MeterService : MeterReadingService.MeterReadingServiceBase
    {
        private readonly ILogger<MeterService> _logger;

        public MeterService(ILogger<MeterService> logger)
        {
            _logger = logger;
        }

        public override Task<StatusMessage> AddReading(ReadingPacket request, ServerCallContext context)
        {
            var result = new StatusMessage { Status = ReadingStatus.Failure };

            if (request.Status != ReadingStatus.Success)
                return Task.FromResult(result);

            try
            {
                foreach (var reading in request.Readings)
                {
                    if (reading.ReadingValue < 1000)
                    {
                        _logger.LogDebug("Reading value is below acceptable value");
                        var trailer = new Metadata
                        {
                            {"BadValue", reading.ReadingValue.ToString()},
                            {"Field", nameof(reading.ReadingValue)},
                            {"Message", "Reading is invalid"}
                        };
                        throw new RpcException(new Status(StatusCode.OutOfRange, "Value is too low"), trailer);
                    }

                    //save to db
                    result.Status = ReadingStatus.Success;
                    _logger.LogInformation($"Reading saved {JsonSerializer.Serialize(reading)}");

                }
            }
            catch (RpcException)
            {
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception thrown during saving of readings: {e}");
                throw new RpcException(Status.DefaultCancelled, e.Message);
            }

            return Task.FromResult(result);
        }

        public override async Task<Empty> SendDiagnostics(IAsyncStreamReader<ReadingMessage> requestStream, ServerCallContext context)
        {
            var task = Task.Run(async () =>
            {
                await foreach (var reading in requestStream.ReadAllAsync())
                {
                    _logger.LogInformation($"Received diagnostic reading {JsonSerializer.Serialize(reading)}");
                }
            });

            await task;

            return new Empty();
        }

        [AllowAnonymous]
        public override Task<TokenResponse> CreateToken(TokenRequest request, ServerCallContext context)
        {
            return Task.FromResult(new TokenResponse
            {
                Token = "token",
                Success = true,
                Expiration = DateTime.UtcNow.AddHours(1).ToTimestamp()
            });
        }
    }
}