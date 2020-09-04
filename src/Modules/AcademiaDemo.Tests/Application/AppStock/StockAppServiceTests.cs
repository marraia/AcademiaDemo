using AcademiaDemo.Application.AppStock;
using AcademiaDemo.Domain.Entities;
using AcademiaDemo.Domain.Interfaces.Repository;
using AcademiaDemo.Tests.Comum;
using FluentAssertions;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AcademiaDemo.Tests.AppStock
{
    public class StockAppServiceTests
    {
        private IStockRepository subStockRepository;

        public StockAppServiceTests()
        {
            this.subStockRepository = Substitute.For<IStockRepository>();
        }

        private StockAppService CreateService()
        {
            return new StockAppService(
                this.subStockRepository);
        }

        [Fact]
        public async Task GetByIdAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid id = Guid.NewGuid();
            Stock stock = GenerateStockFaker.CreateOneStockObject(id);

            subStockRepository
                .GetByIdAsync(id)
                .Returns(stock);

            // Act
            var result = await service.GetByIdAsync(id);

            // Assert
            result
                .Should()
                .BeOfType<Stock>();

            result.Id.Should().NotBe(Guid.Empty);
            result.Ammount.Should().Be(stock.Ammount);
            result.ItemId.Should().Be(stock.ItemId);

            await subStockRepository
                    .Received(1)
                    .GetByIdAsync(Arg.Any<Guid>())
                    .ConfigureAwait(false);
        }

        [Fact]
        public async Task InsertAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid id = Guid.NewGuid();
            int ammount = 10;

            // Act
            await service.InsertAsync(id, ammount);

            // Assert
            await subStockRepository
                    .Received(1)
                    .InsertAsync(Arg.Any<Stock>())
                    .ConfigureAwait(false);
        }

        [Fact]
        public async Task UpdateAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            Guid id = Guid.NewGuid();
            Stock stock = GenerateStockFaker.CreateOneStockObject(id);

            subStockRepository
                .GetByIdAsync(id)
                .Returns(stock);

            // Act
            var result = await service.UpdateAsync(id);

            // Assert
            await subStockRepository
                    .Received(1)
                    .UpdateAsync(Arg.Any<Stock>())
                    .ConfigureAwait(false);
        }
    }
}
