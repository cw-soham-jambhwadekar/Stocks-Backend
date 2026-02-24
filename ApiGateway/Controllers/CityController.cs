using Microsoft.AspNetCore.Mvc;

[ApiController]
public class CityController : ControllerBase
{
    private readonly ICityLogic _cityLogic;

    public CityController(ICityLogic cityLogic)
    {
        _cityLogic = cityLogic;
    }

    [HttpGet("api/cities")]
    public async Task<IActionResult> GetCities()
    {
        try
        {
            var cities = await _cityLogic.GetCitiesAsync();
            return Ok(cities);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
