using System;
using System.Net.Http;
using System.Threading.Tasks;
using Domain;
using Domain.Repository;

namespace GrpcApplication.Services.Impl
{
    public class ScraperService : IScrapperService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IHttpClientFactory _clientFactory;

        public ScraperService(
            IApplicationRepository applicationRepository,
            IHttpClientFactory clientFactory)
        {
            _applicationRepository = applicationRepository ?? 
                                     throw new ArgumentNullException(nameof(IApplicationRepository));
            _clientFactory = clientFactory ?? 
                             throw new ArgumentNullException(nameof(IHttpClientFactory));
        }

        public async Task ParseApplication(Application application)
        {
            var client = _clientFactory.CreateClient();
            
            var document = await new ApplicationDetailsBuilder(application)
                .GetDocument(client);
            
            var details = document
                .ParseName()
                .ParseInstallCount()
                .Build();

            await _applicationRepository.AddDetails(application, details);
        }
    }
}