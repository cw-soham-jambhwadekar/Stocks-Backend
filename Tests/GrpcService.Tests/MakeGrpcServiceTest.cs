using Xunit;
using Moq;
using FluentAssertions;
using Contracts;
using Grpc.Core;

public class MakeGrpcServiceTests
{
    private readonly Mock<IMakeGrpcLogic> _logicMock;
    private readonly MakeGrpcService _service;

    public MakeGrpcServiceTests()
    {
        _logicMock = new Mock<IMakeGrpcLogic>();
        _service = new MakeGrpcService(_logicMock.Object);
    }

    [Fact]
    public async Task GetMakes_Should_CallLogic_And_ReturnResult()
    {
        var expectedResponse = new MakeResponse();
        expectedResponse.Makes.Add(new Make
        {
            MakeId = 1,
            MakeName = "Toyota"
        });

        _logicMock
            .Setup(l => l.GetMakesAsync())
            .ReturnsAsync(expectedResponse);

        var response = await _service.GetMakes(
            new MakeRequest(),
            Mock.Of<ServerCallContext>());

        response.Should().BeSameAs(expectedResponse);
        _logicMock.Verify(l => l.GetMakesAsync(), Times.Once);
    }
}