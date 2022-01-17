using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.UnitTests.Services;

public class CatalogItemServiceTest
{
    private readonly ICatalogItemService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;
    private readonly Mock<IMapper> _mapper;

    private readonly CatalogItem _testItem = new CatalogItem()
    {
        Id = 1,
        Name = "Name",
        Description = "Description",
        Price = 1000,
        AvailableStock = 100,
        CatalogBrandId = 1,
        CatalogTypeId = 1,
        PictureFileName = "1.png"
    };

    private readonly CatalogItemDto _testItemDto = new CatalogItemDto()
    {
        Name = "Name",
        Description = "Description",
        Price = 1000,
        AvailableStock = 100
    };

    public CatalogItemServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransaction()).Returns(dbContextTransaction.Object);

        _catalogService = new CatalogItemService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.AddAsync(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.AddAsync(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task GetByIdAsync_Success()
    {
        // arrange
        _catalogItemRepository.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_testItem);
        _mapper.Setup(s => s.Map<CatalogItemDto>(_testItem)).Returns(_testItemDto);

        // act
        var result = await _catalogService.GetByIdAsync(1);

        // assert
        result.Should().Be(_testItemDto);
    }

    [Fact]
    public async Task GetByIdAsync_Failed()
    {
        // arrange
        CatalogItem? testResult = null;
        CatalogItemDto? testResultDto = null;
        _catalogItemRepository.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(testResult);
        _mapper.Setup(s => s.Map<CatalogItemDto?>(_testItem)).Returns(testResultDto);

        // act
        var result = await _catalogService.GetByIdAsync(1);

        // assert
        result.Should().Be(testResultDto);
    }

    [Fact]
    public async Task GetByBrandAsync_Success()
    {
        // arrange
        IEnumerable<CatalogItem> testResult = new List<CatalogItem>() { _testItem };
        IEnumerable<CatalogItemDto> testResultDto = new List<CatalogItemDto>() { _testItemDto };
        _catalogItemRepository.Setup(s => s.GetByBrandAsync(It.IsAny<string>())).ReturnsAsync(testResult);
        _mapper.Setup(s => s.Map<IEnumerable<CatalogItemDto>>(testResult)).Returns(testResultDto);

        // act
        var result = await _catalogService.GetByBrandAsync("test");

        // assert
        result.Should().BeSameAs(testResultDto);
    }

    [Fact]
    public async Task GetByBrandAsync_Failed()
    {
        // arrange
        IEnumerable<CatalogItem>? testResult = null;
        IEnumerable<CatalogItemDto>? testResultDto = null;
        _catalogItemRepository.Setup(s => s.GetByBrandAsync(It.IsAny<string>())).ReturnsAsync(testResult);
        _mapper.Setup(s => s.Map<IEnumerable<CatalogItemDto>?>(testResult)).Returns(testResultDto);

        // act
        var result = await _catalogService.GetByBrandAsync("test");

        // assert
        result.Should().BeSameAs(testResultDto);
    }

    [Fact]
    public async Task GetByTypeAsync_Success()
    {
        // arrange
        IEnumerable<CatalogItem> testResult = new List<CatalogItem>() { _testItem };
        IEnumerable<CatalogItemDto> testResultDto = new List<CatalogItemDto>() { _testItemDto };
        _catalogItemRepository.Setup(s => s.GetByTypeAsync(It.IsAny<string>())).ReturnsAsync(testResult);
        _mapper.Setup(s => s.Map<IEnumerable<CatalogItemDto>>(testResult)).Returns(testResultDto);

        // act
        var result = await _catalogService.GetByTypeAsync("test");

        // assert
        result.Should().BeSameAs(testResultDto);
    }

    [Fact]
    public async Task GetByTypeAsync_Failed()
    {
        // arrange
        IEnumerable<CatalogItem>? testResult = null;
        IEnumerable<CatalogItemDto>? testResultDto = null;
        _catalogItemRepository.Setup(s => s.GetByTypeAsync(It.IsAny<string>())).ReturnsAsync(testResult);
        _mapper.Setup(s => s.Map<IEnumerable<CatalogItemDto>?>(testResult)).Returns(testResultDto);

        // act
        var result = await _catalogService.GetByTypeAsync("test");

        // assert
        result.Should().BeSameAs(testResultDto);
    }

    [Fact]
    public async Task DeleteProductAsync_Success()
    {
        // arrange
        var testResult = true;
        _catalogItemRepository.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.DeleteProductAsync(1);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task DeleteProductAsync_Failed()
    {
        // arrange
        var testResult = false;
        _catalogItemRepository.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.DeleteProductAsync(1);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task UpdateProductAsync_Success()
    {
        // arrange
        var testResult = true;
        _catalogItemRepository.Setup(s => s.Update(
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.UpdateProductAsync(_testItem.Id, _testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task UpdateProductAsync_Failed()
    {
        // arrange
        var testResult = false;
        _catalogItemRepository.Setup(s => s.Update(
            It.IsAny<int>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.UpdateProductAsync(_testItem.Id, _testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }
}