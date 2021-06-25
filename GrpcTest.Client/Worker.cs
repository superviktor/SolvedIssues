using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
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
        private string _token;
        private DateTime _expiration = DateTime.MinValue;

        protected bool NeedsLogin => string.IsNullOrWhiteSpace(_token) || _expiration > DateTime.UtcNow;

        protected MeterReadingService.MeterReadingServiceClient Client
        {
            get
            {
                if (_client == null)
                {
                    var cert = new X509Certificate2(_configuration["Service:CertificateFileName"],
                        _configuration["Service:CertificatePassword"]);
                    var handler = new HttpClientHandler();
                    handler.ClientCertificates.Add(cert);
                    var client = new HttpClient(handler);
                    var opt = new GrpcChannelOptions
                    {
                        HttpClient = client,
                        LoggerFactory = _loggerFactory
                    };
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
                    //for jwt
                    //if (!NeedsLogin || await GenerateToken())
                    //{
                    //    var headers = new Metadata {{"Authorization", $"Bearer {_token}"}};
                    //    var result = await Client.AddReadingAsync(readingPacket, headers);

                    //    _logger.LogInformation(result.Status == ReadingStatus.Success
                    //        ? "Successfully sent"
                    //        : "Failed to send");
                    //}


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

        private async Task<bool> GenerateToken()
        {
            var request = new TokenRequest
            {
                Username = _configuration["Service:Username"],
                Password = _configuration["Service:Password"]
            };

            var response = await Client.CreateTokenAsync(request);

            if (response.Success)
            {
                _token = response.Token;
                _expiration = response.Expiration.ToDateTime();
                return true;
            }

            return false;
        }
    }
}
