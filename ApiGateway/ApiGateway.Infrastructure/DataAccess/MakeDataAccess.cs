using Grpc.Net.Client;
using Contracts;
using System.Linq;

public class MakeDataAccess
{
    private readonly IConfiguration _config;
    private readonly MakeService.MakeServiceClient grpcClient;

    public MakeDataAccess(IConfiguration config)
    {
        _config = config;
        var channel = GrpcChannel.ForAddress(_config.GetConnectionString("GrpcConnection")!); 
        grpcClient = new MakeService.MakeServiceClient(channel);
    }

    public async Task<MakeResponse> GetMakesAsync()
    {
        var grpcResult = await grpcClient.GetMakesAsync(new MakeRequest());
        return grpcResult;
    }
}
