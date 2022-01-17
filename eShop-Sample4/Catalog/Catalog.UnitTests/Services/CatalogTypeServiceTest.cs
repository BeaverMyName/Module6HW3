using Catalog.Host.Data.Entities;

namespace Catalog.UnitTests.Services
{
    public class CatalogTypeServiceTest
    {
        private readonly ICatalogTypeService _catalogService;

        private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogService>> _logger;
        private readonly Mock<IMapper> _mapper;

        private readonly CatalogType _testType = new CatalogType() { Id = 1, Type = "TestName" };

        public CatalogTypeServiceTest()
        {
            _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogService>>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransaction()).Returns(dbContextTransaction.Object);

            _catalogService = new CatalogTypeService(_dbContextWrapper.Object, _logger.Object, _catalogTypeRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task CreateTypeAsync_Success()
        {
            // arrange
            var testResult = 1;
            var testName = "TestName";

            _catalogTypeRepository.Setup(s => s.Add(It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogService.CreateTypeAsync(testName);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task CreateTypeAsync_Failed()
        {
            // arrange
            int? testResult = null;
            var testName = "TestName";

            _catalogTypeRepository.Setup(s => s.Add(It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogService.CreateTypeAsync(testName);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task DeleteTypeAsync_Success()
        {
            // arrange
            var testId = 1;
            var testResult = true;

            _catalogTypeRepository.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogService.DeleteTypeAsync(testId);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task DeleteTypeAsync_Failed()
        {
            // arrange
            int testId = 1;
            var testResult = false;

            _catalogTypeRepository.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogService.DeleteTypeAsync(testId);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task UpdateTypeAsync_Success()
        {
            // arrange
            var testResult = true;

            _catalogTypeRepository.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogService.UpdateTypeAsync(_testType.Id, _testType.Type);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task UpdateTypeAsync_Failed()
        {
            // arrange
            var testResult = false;

            _catalogTypeRepository.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogService.UpdateTypeAsync(_testType.Id, _testType.Type);

            // assert
            result.Should().Be(testResult);
        }
    }
}
