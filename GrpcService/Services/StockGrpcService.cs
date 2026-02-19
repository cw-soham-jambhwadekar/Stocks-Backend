using Grpc.Core;
using Contracts;

public class StockGrpcService : StockService.StockServiceBase
{
    private readonly IStockRepository _repo;
    private readonly StockMapper _mapper;

    public StockGrpcService(IStockRepository repo)
    {
        _repo = repo;
        _mapper = new StockMapper();
    }

    public override async Task<StockResponse> GetStocks(
        Filters filters,
        ServerCallContext context)
    {
        var entities = await _repo.GetStocksAsync(filters);

        var result = new StockResponse();


        if (entities.Any())
        {
            result.Stocks.AddRange(
                entities.Select(e => _mapper.Map(e))
            );
            
            result.NextPageUrl = "pn=" + (filters.PageNo + 1);
        }

        return result;
    }
}
