public class ResponseDto
{
    public List<StockDto> Stocks { get; set; } = new();
    public string NextPageUrl { get; set; } = "";
}