using Grpc.Core;
using Contracts;
using System.Text.Json;

public class StockGrpcService : StockService.StockServiceBase
{
    private readonly IStockGrpcLogic _stockLogic;
    private readonly FiltersMapper _filtersMapper;
    public StockGrpcService(IStockGrpcLogic stockLogic , FiltersMapper filtersMapper)
    {
        _filtersMapper = filtersMapper;
        _stockLogic = stockLogic;
    }

    public override async Task<StockResponse> GetStocks(
        Filters filters,
        ServerCallContext context)
    {
        var result = await _stockLogic.GetStocksAsync(_filtersMapper.Map(filters));
        return result;
    }
}
