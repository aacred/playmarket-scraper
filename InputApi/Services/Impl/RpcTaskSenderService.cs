using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;

namespace InputApi.Services.Impl
{
    public class RpcTaskSenderService : ITaskSenderService
    {
        private readonly string _connectionString = Environment.GetEnvironmentVariable("GRPC_CONNECTION_STRING");
        
        public async Task ScrapeDetails(int applicationId)
        {
            var channel = GrpcChannel.ForAddress(_connectionString, new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Insecure
            });
            var client = new TaskSender.TaskSenderClient(channel);
            
            var response = await client
                .GetDetailsAsync(new DetailsRequest() { Id = applicationId });
        }
    }
}