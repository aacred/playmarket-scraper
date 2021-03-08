using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Common.Exceptions;
using Domain.Repository;
using InputApi.Model.Dto;
using InputApi.Repository.Mapper;
using Microsoft.EntityFrameworkCore;

namespace InputApi.Services.Impl
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ITaskSenderService _taskSenderService;
        private readonly IDataMapper _dataMapper;

        public ApplicationService(
            IApplicationRepository applicationRepository, 
            ITaskSenderService taskSenderService,
            IDataMapper dataMapper)
        {
            _applicationRepository = applicationRepository ?? throw new ArgumentNullException(nameof(IApplicationRepository));
            _taskSenderService = taskSenderService ?? throw new ArgumentNullException(nameof(ITaskSenderService));
            _dataMapper = dataMapper ?? throw new ArgumentNullException(nameof(IDataMapper));
        }
        
        public async Task<int> AddOrGetId(string url)
        {
            var existedItem = await _applicationRepository
                .GetAll()
                .SingleOrDefaultAsync(x => x.Url.Equals(url));
            
            if (existedItem != null)
            {
                return existedItem.ApplicationId;
            }


            var newItem = await _applicationRepository.AddAsync(new Application() {Url = url});
            await _taskSenderService.ScrapeDetails(newItem.ApplicationId);
            return newItem.ApplicationId;
        }

        public async Task<ApplicationDetailsDto> GetDetails(int id)
        {
            var application = await _applicationRepository
                .GetAll()
                .Include(x => x.Details)
                .SingleOrDefaultAsync(x => x.ApplicationId.Equals(id));

            if (application == null)
                throw new ApplicationNotFoundException(
                    $"Application (id: {id}) not registered.");

            if (!application.Details.Any())
                throw new ApplicationDetailsNotFoundException(
                    $"Application (id: {id}) details haven't been created yet.");
            
            return _dataMapper.GetMapper()
                    .Map<ApplicationDetails, ApplicationDetailsDto>(application.Details.Last());
        }
    }
}