using Microsoft.AspNetCore.Mvc;

[ApiController]
public class MakeController : ControllerBase
{
    private readonly IMakeLogic _makeLogic;

    public MakeController(IMakeLogic makeLogic)
    {
        _makeLogic = makeLogic;
    }

    [HttpGet("api/makes")]
    public async Task<IActionResult> GetMakes()
    {
        try
        {
            var makes = await _makeLogic.GetMakesAsync();
            return Ok(makes);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

}
