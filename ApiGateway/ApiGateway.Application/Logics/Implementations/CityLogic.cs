using Grpc.Net.Client;
using Contracts;
using System.Linq;

public class CityLogic : ICityLogic
{
    private readonly CityDataAccess _cityDataLayer;
    private readonly CityMapper cityMapper;

    public CityLogic(CityDataAccess cityDataLayer , CityMapper _cityMapper)
    {
        _cityDataLayer = cityDataLayer;
        cityMapper = _cityMapper;
    }

    public async Task<CitiesResponseDto> GetCitiesAsync()
    {
        var grpcResult = await _cityDataLayer.GetCitiesAsync();
        var response = new CitiesResponseDto();
        response.Cities = grpcResult.Cities.Select(city => cityMapper.Map(city)).ToList();
        return response;
    }
}
