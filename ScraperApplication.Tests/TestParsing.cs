using System;
using System.Net.Http;
using System.Threading.Tasks;
using Domain;
using Domain.Repository;
using Grpc.Net.Client;
using GrpcApplication;
using GrpcApplication.Services;
using GrpcApplication.Services.Impl;
using Moq;
using Xunit;

namespace ScaperApplication.Tests
{
    public class TestParsing
    {
        private readonly Application _application;
        
        public TestParsing()
        {
            _application = new Application()
            {
                ApplicationId = 1,
                Url = "https://play.google.com/store/apps/details?id=com.instagram.android&hl=ru&gl=US"
            };
        }
        
        [Fact]
        public async Task TestApplicationDetailsBuilder()
        {
            HttpClient client = new HttpClient();
            
            var document = await new ApplicationDetailsBuilder(_application)
                .GetDocument(client);
            
            var app = document
                .ParseName()
                .ParseInstallCount()
                .Build();

            Assert.NotNull(app.Name);
            Assert.Equal(app.Name, "Instagram");
            Assert.True(app.DownloadCount >= 3386285326);
        }
        
        [Fact]
        public void TestScraperInstgram()
        {
            var clientFactory = new Mock<IHttpClientFactory>();
            var repo = new Mock<IApplicationRepository>();
            repo.Setup(x => x.AddDetails(
                    It.IsAny<Application>(), 
                    It.IsAny<ApplicationDetails>()))
                .Verifiable();

            IScrapperService scrapper = new ScraperService(repo.Object, clientFactory.Object);
            scrapper.ParseApplication(_application);
        }
    }
}