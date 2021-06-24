using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
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
        private readonly ILoggerFactory _loggerFactory;
        private MeterReadingService.MeterReadingServiceClient _client;

        protected MeterReadingService.MeterReadingServiceClient Client
        {
            get
            {
                if (_client == null)
                {
                    var opt = new GrpcChannelOptions() { LoggerFactory = _loggerFactory };
                    var channel = GrpcChannel.ForAddress(_configuration.GetValue<string>("Service:ServerUrl"), opt);
                    _client = new MeterReadingService.MeterReadingServiceClient(channel);
                }

                return _client;
            }
        }

        public Worker(ILogger<Worker> logger, IConfiguration configuration, ReadingFactory readingFactory, ILoggerFactory loggerFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _readingFactory = readingFactory;
            _loggerFactory = loggerFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var counter = 0;
            var customerId = _configuration.GetValue<int>("Service:CustomerId");

            while (!stoppingToken.IsCancellationRequested)
            {
                counter++;
                if (counter % 10 == 0)
                {
                    _logger.LogInformation("Sending diagnostics");
                    var stream = Client.SendDiagnostics();
                    for (var i = 0; i < 5; i++)
                    {
                        var reading = await _readingFactory.Generate(customerId);
                        await stream.RequestStream.WriteAsync(reading);
                    }

                    await stream.RequestStream.CompleteAsync();
                }
                var readingPacket = new ReadingPacket
                {
                    Status = ReadingStatus.Success,
                    Notes = "test"
                };
                for (var i = 0; i < 5; i++)
                {
                    readingPacket.Readings.Add(await _readingFactory.Generate(customerId));
                }

                try
                {
                    var result = await Client.AddReadingAsync(readingPacket);

                    _logger.LogInformation(result.Status == ReadingStatus.Success
                        ? "Successfully sent"
                        : "Failed to send");

                }
                catch (RpcException e)
                {
                    if (e.StatusCode == StatusCode.OutOfRange)
                        _logger.LogError($" {e.Trailers}");
                    _logger.LogError($"Exception thrown {e}");
                }

                await Task.Delay(_configuration.GetValue<int>("Service:DelayInterval"), stoppingToken);
            }
        }
    }
}
