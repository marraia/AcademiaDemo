using AcademiaDemo.Application.AppItem;
using AcademiaDemo.Application.AppItem.Input;
using AcademiaDemo.Domain.Interfaces.Repository;
using AcademiaDemo.Tests.Comum;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using AcademiaDemo.Domain.Entities;

namespace AcademiaDemo.Tests.AppItem
{
    public class ItemAppServiceTests
    {
        private IItemRepository subItemRepository;
        private IStockRepository subStockRepository;

        public ItemAppServiceTests()
        {
            this.subItemRepository = Substitute.For<IItemRepository>();
            this.subStockRepository = Substitute.For<IStockRepository>();
        }

        private ItemAppService CreateService()
        {
            return new ItemAppService(
                this.subItemRepository,
                this.subStockRepository);
        }

        [Fact]
        public async Task GetAllAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            var collectionsItem = GenerateItemFaker.CreateManyItemObject(10);

            this.subItemRepository
                    .GetAllAsync()
                    .Returns(collectionsItem);

            // Act
            var result = await service
                                .GetAllAsync();

            // Assert
            result
                .Should()
                .NotBeNull();
            
            result
                .Should()
                .HaveCount(10);

        }

        [Fact]
        public async Task InsertAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            ItemInput input = GenerateItemFaker.CreateItemInputObject();

            // Act
            var result = await service
                                .InsertAsync(input);

            // Assert
            result
                .Should()
                .BeOfType<Item>();

            result.Id.Should().NotBe(Guid.Empty);
            result.Description.Should().Be(input.Description);
            result.Price.Should().Be(input.Price);

            await subItemRepository
                    .Received(1)
                    .InsertAsync(Arg.Any<Item>())
                    .ConfigureAwait(false);
        }
    }
}
