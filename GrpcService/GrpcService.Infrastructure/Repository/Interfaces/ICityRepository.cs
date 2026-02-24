using Contracts;

public interface ICityRepository
{
    Task<IEnumerable<CityEntity>> GetCitiesAsync();
}
