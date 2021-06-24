using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using GrpcTest.Api.Protos;
using Microsoft.Extensions.Configuration;

namespace GrpcTest.Client
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly ReadingFactory _readingFactory;
        private MeterReadingService.MeterReadingServiceClient _client;

        protected MeterReadingService.MeterReadingServiceClient Client
        {
            get
            {
                if (_client == null)
                {
                    var channel = GrpcChannel.ForAddress(_configuration.GetValue<string>("Service:ServerUrl"));
                    _client = new MeterReadingService.MeterReadingServiceClient(channel);
                }

                return _client;
            }
        }

        public Worker(ILogger<Worker> logger, IConfiguration configuration, ReadingFactory readingFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _readingFactory = readingFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var customerId = _configuration.GetValue<int>("Service:CustomerId");
                var readingPacket = new ReadingPacket
                {
                    Status = ReadingStatus.Success,
                    Notes = "test"
                };
                for (var i = 0; i < 5; i++)
                {
                    readingPacket.Readings.Add(await _readingFactory.Generate(customerId));
                }
                var result = await Client.AddReadingAsync(readingPacket);

                _logger.LogInformation(result.Status == ReadingStatus.Success 
                    ? "Successfully sent" 
                    : "Failed to send");

                await Task.Delay(_configuration.GetValue<int>("Service:DelayInterval"), stoppingToken);
            }
        }
    }
}
