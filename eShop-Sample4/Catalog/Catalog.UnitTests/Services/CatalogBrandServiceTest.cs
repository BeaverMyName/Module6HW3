using Catalog.Host.Data.Entities;

namespace Catalog.UnitTests.Services
{
    public class CatalogBrandServiceTest
    {
        private readonly ICatalogBrandService _catalogService;

        private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogService>> _logger;
        private readonly Mock<IMapper> _mapper;

        private readonly CatalogBrand _testBrand = new CatalogBrand() { Id = 1, Brand = "TestName" };

        public CatalogBrandServiceTest()
        {
            _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogService>>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransaction()).Returns(dbContextTransaction.Object);

            _catalogService = new CatalogBrandService(_dbContextWrapper.Object, _logger.Object, _catalogBrandRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task CreateBrandAsync_Success()
        {
            // arrange
            var testResult = 1;
            var testName = "TestName";

            _catalogBrandRepository.Setup(s => s.Add(It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogService.CreateBrandAsync(testName);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task CreateBrandAsync_Failed()
        {
            // arrange
            int? testResult = null;
            var testName = "TestName";

            _catalogBrandRepository.Setup(s => s.Add(It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogService.CreateBrandAsync(testName);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task DeleteBrandAsync_Success()
        {
            // arrange
            var testId = 1;
            var testResult = true;

            _catalogBrandRepository.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogService.DeleteBrandAsync(testId);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task DeleteBrandAsync_Failed()
        {
            // arrange
            int testId = 1;
            var testResult = false;

            _catalogBrandRepository.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogService.DeleteBrandAsync(testId);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task UpdateBrandAsync_Success()
        {
            // arrange
            var testResult = true;

            _catalogBrandRepository.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogService.UpdateBrandAsync(_testBrand.Id, _testBrand.Brand);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task UpdateBrandAsync_Failed()
        {
            // arrange
            var testResult = false;

            _catalogBrandRepository.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogService.UpdateBrandAsync(_testBrand.Id, _testBrand.Brand);

            // assert
            result.Should().Be(testResult);
        }
    }
}
