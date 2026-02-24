using Grpc.Core;
using Contracts;

public class CityGrpcLogic : ICityGrpcLogic
{
    private readonly ICityRepository _cityRepository;
    private readonly CityMapper cityMapper;

    public CityGrpcLogic(ICityRepository cityRepository , CityMapper _cityMapper)
    {
        _cityRepository = cityRepository;
        cityMapper = _cityMapper;
    }

    public async Task<CityResponse> GetCitiesAsync()
    {
        var cities = await _cityRepository.GetCitiesAsync();
        var result = new CityResponse();
        result.Cities.AddRange( cities.Select(e => cityMapper.Map(e)));
        return result;
    }
}
