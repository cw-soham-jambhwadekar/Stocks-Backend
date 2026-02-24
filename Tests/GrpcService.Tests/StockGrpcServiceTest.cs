using Xunit;
using Moq;
using FluentAssertions;
using Contracts;
using Grpc.Core;

public class StockGrpcServiceTests
{
    private readonly Mock<IStockGrpcLogic> _logicMock;
    private readonly FiltersMapper _realFiltersMapper;
    private readonly StockGrpcService _service;

    public StockGrpcServiceTests()
    {
        _logicMock = new Mock<IStockGrpcLogic>();

        _realFiltersMapper = new FiltersMapper();

        _service = new StockGrpcService(
            _logicMock.Object,
            _realFiltersMapper
        );
    }

    [Fact]
    public async Task GetStocks_Should_MapFilters_And_ReturnLogicResult()
    {
        var grpcFilters = new Filters { PageNo = 1 };

        var expectedResponse = new StockResponse();
        expectedResponse.Stocks.Add(new Stock());

        _logicMock
            .Setup(l => l.GetStocksAsync(It.Is<FiltersEntity>(f => f.PageNo == 1)))
            .ReturnsAsync(expectedResponse);

        var response = await _service.GetStocks(
            grpcFilters,
            Mock.Of<ServerCallContext>());

        response.Should().BeSameAs(expectedResponse);

        _logicMock.Verify(
            l => l.GetStocksAsync(It.Is<FiltersEntity>(f => f.PageNo == 1)),
            Times.Once);
    }
}