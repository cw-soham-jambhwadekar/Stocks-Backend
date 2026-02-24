using Contracts;

public interface IMakeRepository
{
    Task<IEnumerable<MakeEntity>> GetMakesAsync();
}
