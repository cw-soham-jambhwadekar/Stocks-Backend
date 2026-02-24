using Microsoft.AspNetCore.Mvc;

[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockLogic _stockLogic;

    public StockController(IStockLogic stockLogic)
    {
        _stockLogic = stockLogic;
    }

    [HttpGet("api/stocks")]
    public async Task<IActionResult> GetStocks(
        [FromQuery] FiltersDto filtersDto)
    {
        try
        {
            var result = await _stockLogic.GetStocksAsync(filtersDto);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

}
