using Xunit;
using Moq;
using FluentAssertions;
using Contracts;
using Grpc.Core;

public class CityGrpcServiceTests
{
    private readonly Mock<ICityGrpcLogic> _logicMock;
    private readonly CityGrpcService _service;

    public CityGrpcServiceTests()
    {
        _logicMock = new Mock<ICityGrpcLogic>();
        _service = new CityGrpcService(_logicMock.Object);
    }

    [Fact]
    public async Task GetCities_Should_CallLogic_And_ReturnResult()
    {
        var expectedResponse = new CityResponse();
        expectedResponse.Cities.Add(new City
        {
            CityId = 1,
            CityName = "Mumbai"
        });

        _logicMock
            .Setup(l => l.GetCitiesAsync())
            .ReturnsAsync(expectedResponse);

        var response = await _service.GetCities(
            new CityRequest(),
            Mock.Of<ServerCallContext>());

        response.Should().BeSameAs(expectedResponse);
        _logicMock.Verify(l => l.GetCitiesAsync(), Times.Once);
    }
}