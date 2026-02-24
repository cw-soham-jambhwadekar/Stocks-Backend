using Grpc.Net.Client;
using Contracts;
using System.Linq;

public class StockLogic : IStockLogic
{
    private readonly StockDataAccess _stockDataLayer;
    private readonly StockMapper stockMapper;
    private readonly IFiltersTranslator _translator;

    public StockLogic(IConfiguration config , 
    StockDataAccess stockDataLayer , StockMapper _stockMapper ,  IFiltersTranslator translator)
    {
        _stockDataLayer = stockDataLayer;
        stockMapper = _stockMapper;
        _translator = translator;
    }

    public async Task<StocksResponseDto> GetStocksAsync(FiltersDto filtersDto)
    {
        var filters = _translator.Translate(filtersDto);
        var grpcResult = await _stockDataLayer.GetStocksAsync(filters);
        var response = new StocksResponseDto();

        response.Stocks = grpcResult.Stocks.Select(stock => stockMapper.ToDto(stock)).ToList();

        if (response.Stocks.Any())
        {
            response.NextPageUrl = UrlBuilder.BuildNextPageUrl(filters , "/stocks");
        }
        return response;
    }
}
