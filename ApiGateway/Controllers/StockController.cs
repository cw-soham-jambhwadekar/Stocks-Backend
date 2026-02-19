using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/stocks")]
public class StockController : ControllerBase
{
    private readonly StockClient _client;

    public StockController(StockClient client)
    {
        _client = client;
    }

    [HttpGet]
    public async Task<IActionResult> GetStocks(
        [FromQuery] FiltersDto dto,
        [FromServices] IFiltersTranslator translator)
    {
        var filters = translator.Translate(dto);
        var result = await _client.GetStocksAsync(filters);
        return Ok(result);
    }
}
