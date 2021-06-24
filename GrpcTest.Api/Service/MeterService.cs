﻿using System;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcTest.Api.Protos;
using Microsoft.Extensions.Logging;

namespace GrpcTest.Api.Service
{
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
                    //save to db
                    result.Status = ReadingStatus.Success;
                    _logger.LogInformation($"Reading saved {JsonSerializer.Serialize(reading)}");

                }
            }
            catch (Exception e)
            {
                result.Message = $"Exception thrown during process: {e.Message}";
                _logger.LogError(e.Message);
            }

            return Task.FromResult(result);
        }
    }
}