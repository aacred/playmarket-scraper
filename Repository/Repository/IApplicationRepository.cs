using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IApplicationRepository : IRepository<Application>
    {
        Task AddDetails(Application application, ApplicationDetails details);
    }
}