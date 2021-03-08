using System.Threading.Tasks;

namespace InputApi.Services
{
    public interface ITaskSenderService
    {
        Task ScrapeDetails(int applicationId);
    }
}