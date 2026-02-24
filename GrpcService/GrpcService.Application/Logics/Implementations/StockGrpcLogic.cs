using Grpc.Core;
using Contracts;
using System.Text.Json;

public class StockGrpcLogic : IStockGrpcLogic
{
    private readonly IStockRepository _stockRepository;
    private readonly StockMapper stockMapper;

    public StockGrpcLogic(IStockRepository stockRepository , MakeMapper _makeMapper)
    {
        _stockRepository = stockRepository;
        stockMapper = new StockMapper();
    }

    public async Task<StockResponse> GetStocksAsync(FiltersEntity filters)
    {
      
        var entities = await _stockRepository.GetStocksAsync(filters);

        var result = new StockResponse();

        if (entities.Any())
        {
            result.Stocks.AddRange(
                entities.Select(e => stockMapper.Map(e))
            );
        }

        return result;
    }
}
