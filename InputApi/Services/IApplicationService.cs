using System.Threading.Tasks;
using Domain;
using InputApi.Model.Dto;

namespace InputApi.Services
{
    public interface IApplicationService
    {
        Task<int> AddOrGetId(string url);
        Task<ApplicationDetailsDto> GetDetails(int id);
    }
}