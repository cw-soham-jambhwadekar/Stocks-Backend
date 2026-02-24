using Grpc.Net.Client;
using Contracts;
using System.Linq;

public class CityDataAccess
{
    private readonly IConfiguration _config;
    private readonly CityService.CityServiceClient grpcClient;

    public CityDataAccess(IConfiguration config)
    {
        _config = config;
        var channel = GrpcChannel.ForAddress(_config.GetConnectionString("GrpcConnection")!); 
        grpcClient = new CityService.CityServiceClient(channel);
    }

    public async Task<CityResponse> GetCitiesAsync()
    {
        var grpcResult = await grpcClient.GetCitiesAsync(new CityRequest());
        return grpcResult;
    }
}
