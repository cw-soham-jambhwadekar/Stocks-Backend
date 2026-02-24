using Contracts;

public interface ICityLogic
{
   Task<CitiesResponseDto> GetCitiesAsync();
}