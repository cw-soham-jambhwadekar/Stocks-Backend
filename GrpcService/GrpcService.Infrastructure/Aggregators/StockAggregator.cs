public class StockAggregator
{
    private readonly Dictionary<string, StockEntity> _dict = new();

    public StockEntity Map(StockEntity stock, ImageRow image)
    {
        if (!_dict.TryGetValue(stock.ProfileId, out var existing))
        {
            existing = stock;
            existing.StockImages = new List<string>();
            _dict.Add(existing.ProfileId, existing);
        }

        if (!string.IsNullOrEmpty(image?.ImageUrl))
            existing.StockImages.Add(image.ImageUrl);

        return existing;
    }

    public IEnumerable<StockEntity> Result => _dict.Values;
}