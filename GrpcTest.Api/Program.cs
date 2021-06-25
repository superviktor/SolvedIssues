using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GrpcTest.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(o =>
                    {
                        var config = o.ApplicationServices.GetService<IConfiguration>();
                        var cert = new X509Certificate2(config["Certificate:File"], config["Certificate:Password"]);
                        o.ConfigureHttpsDefaults(h =>
                        {
                            h.ClientCertificateMode = ClientCertificateMode.AllowCertificate;
                            h.CheckCertificateRevocation = false;
                            h.ServerCertificate = cert;
                        });
                    });
                });
    }
}
