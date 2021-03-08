using System.Threading.Tasks;
using Domain;

namespace GrpcApplication.Services
{
    public interface IScrapperService
    {
        Task ParseApplication(Application app);
    }
}