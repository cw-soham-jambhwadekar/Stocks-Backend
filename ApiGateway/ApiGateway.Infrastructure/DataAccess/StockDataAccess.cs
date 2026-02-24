using Grpc.Net.Client;
using Contracts;
using System.Linq;

public class StockDataAccess
{
    private readonly IConfiguration _config;
    private readonly StockService.StockServiceClient grpcClient;
    private readonly FiltersMapper filtersMapper;

    public StockDataAccess(IConfiguration config , FiltersMapper _filtersMapper)
    {
        _config = config;
        var channel = GrpcChannel.ForAddress(_config.GetConnectionString("GrpcConnection")!); 
        grpcClient = new StockService.StockServiceClient(channel);
        filtersMapper = _filtersMapper;
    }

    public async Task<StockResponse> GetStocksAsync(FiltersEntity filters)
    {
        var grpcResult = await grpcClient.GetStocksAsync(filtersMapper.Map(filters));
        return grpcResult;
    }
}
