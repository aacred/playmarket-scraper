using System.Linq;
using System.Threading.Tasks;
using Domain.Repository;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrpcApplication.Services
{
    public class ApplicationService : TaskSender.TaskSenderBase
    {
        private readonly ILogger<ApplicationService> _logger;
        private readonly IScrapperService _scrapperService;
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(
            ILogger<ApplicationService> logger, 
            IScrapperService scrapperService, 
            IApplicationRepository applicationRepository)
        {
            _logger = logger;
            _scrapperService = scrapperService;
            _applicationRepository = applicationRepository;
        }

        public override async Task<DetailsReply> GetDetails(DetailsRequest request, ServerCallContext context)
        {
            var application = _applicationRepository
                .GetAll()
                .Include(x => x.Details)
                .SingleOrDefault(x => x.ApplicationId == request.Id);

            if (application == null)
            {
                return new DetailsReply()
                {
                    Result = "FAULT",
                    Found = false
                };
            }

            await _scrapperService.ParseApplication(application);
            
            return new DetailsReply()
            {
                Result = "SUCCESS",
                Found = true
            };
        }
    }
}