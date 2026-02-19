using Grpc.Net.Client;
using Contracts;
using System.Linq;

public class StockClient
{
    private readonly IConfiguration _config;
    private readonly StockService.StockServiceClient grpcClient;
    private readonly FiltersMapper filtersMapper;
    private readonly StockMapper stockMapper;

    public StockClient(IConfiguration config , StockMapper _stockMapper)
    {
        _config = config;
        var channel = GrpcChannel.ForAddress(_config.GetConnectionString("GrpcConnection")!); 
        grpcClient = new StockService.StockServiceClient(channel);
        filtersMapper = new FiltersMapper();
        stockMapper = _stockMapper;
    }

    public async Task<ResponseDto> GetStocksAsync(FiltersEntity filters)
    {
        var grpcResult = await grpcClient.GetStocksAsync(filtersMapper.Map(filters));
        var response = new ResponseDto();

        response.Stocks = grpcResult.Stocks.Select(stock => stockMapper.ToDto(stock)).ToList();
        response.NextPageUrl = grpcResult.NextPageUrl;
        return response;
    }
}
