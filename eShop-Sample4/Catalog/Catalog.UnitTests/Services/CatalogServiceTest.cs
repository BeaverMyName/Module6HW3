using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTest
{
    private readonly ICatalogService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
    private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    public CatalogServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
        _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransaction()).Returns(dbContextTransaction.Object);

        _catalogService = new CatalogService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _catalogTypeRepository.Object, _catalogBrandRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;

        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItem>()
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Name = "TestName",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogItemSuccess = new CatalogItem()
        {
            Name = "TestName"
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Name = "TestName"
        };

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetBrandsAsync_Success()
    {
        // arrange
        var catalogBrands = new List<CatalogBrand>() { new CatalogBrand() { Brand = "TestName" } };
        var catalogBrandsDto = new List<CatalogBrandDto>() { new CatalogBrandDto() { Brand = "TestName" } };
        _catalogBrandRepository.Setup(s => s.GetBrandsAsync()).ReturnsAsync(catalogBrands);
        _mapper.Setup(s => s.Map<IEnumerable<CatalogBrandDto>>(catalogBrands)).Returns(catalogBrandsDto);

        // act
        var result = await _catalogService.GetBrandsAsync();

        // assert
        result.Should().BeSameAs(catalogBrandsDto);
    }

    [Fact]
    public async Task GetBrandsAsync_Failed()
    {
        // arrange
        IEnumerable<CatalogBrand>? catalogBrands = null;
        IEnumerable<CatalogBrandDto>? catalogBrandsDto = null;
        _catalogBrandRepository.Setup(s => s.GetBrandsAsync()).ReturnsAsync(catalogBrands);
        _mapper.Setup(s => s.Map<IEnumerable<CatalogBrandDto>?>(catalogBrands)).Returns(catalogBrandsDto);

        // act
        var result = await _catalogService.GetBrandsAsync();

        // assert
        result.Should().BeSameAs(catalogBrandsDto);
    }

    [Fact]
    public async Task GetTypesAsync_Success()
    {
        // arrange
        var catalogTypes = new List<CatalogType>() { new CatalogType() { Type = "TestName" } };
        var catalogTypesDto = new List<CatalogTypeDto>() { new CatalogTypeDto() { Type = "TestName" } };
        _catalogTypeRepository.Setup(s => s.GetTypesAsync()).ReturnsAsync(catalogTypes);
        _mapper.Setup(s => s.Map<IEnumerable<CatalogTypeDto>>(catalogTypes)).Returns(catalogTypesDto);

        // act
        var result = await _catalogService.GetTypesAsync();

        // assert
        result.Should().BeSameAs(catalogTypesDto);
    }

    [Fact]
    public async Task GetTypesAsync_Failed()
    {
        // arrange
        IEnumerable<CatalogType>? catalogTypes = null;
        IEnumerable<CatalogTypeDto>? catalogTypesDto = null;
        _catalogTypeRepository.Setup(s => s.GetTypesAsync()).ReturnsAsync(catalogTypes);
        _mapper.Setup(s => s.Map<IEnumerable<CatalogTypeDto>?>(catalogTypes)).Returns(catalogTypesDto);

        // act
        var result = await _catalogService.GetTypesAsync();

        // assert
        result.Should().BeSameAs(catalogTypesDto);
    }
}