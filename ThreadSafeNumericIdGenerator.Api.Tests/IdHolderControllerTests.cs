using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using ThreadSafeNumericIdGenerator.Api.Tests.Fakes;
using ThreadSafeNumericIdGenerator.Application.Base;
using Xunit;

namespace ThreadSafeNumericIdGenerator.Api.Tests
{
    public class IdHolderControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public IdHolderControllerTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task GetNextId_ReturnsCorrectResult()
        {
            //Arrange 
            var client = factory.WithWebHostBuilder(buidler =>
            {
                buidler.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IIdHolderService, TestIdHolderService>();
                });
            }).CreateClient();
            var expectedResults = Enumerable.Range(1, 100);

            //Act
            var tasks = from n in Enumerable.Range(0, 100) select client.GetAsync("/api/id-holders/viktor/next");
            var startedTasks = tasks.ToList();
            await Task.WhenAll(startedTasks);

            //Assert 
            var readContentTasks = from t in startedTasks select t.Result.Content.ReadAsStringAsync();
            var startedReadContentTasks = readContentTasks.ToList();
            await Task.WhenAll(startedReadContentTasks);
            var ids = startedReadContentTasks.Select(r => long.Parse(r.Result)).OrderBy(r => r);

            ids.Count().Should().Be(100);
            ids.Should().BeEquivalentTo(expectedResults);
        }
    }
}
