using Riok.Mapperly.Abstractions;
using Contracts;

[Mapper]
public partial class CityMapper
{
    public partial CityDto Map(City entity);
}
