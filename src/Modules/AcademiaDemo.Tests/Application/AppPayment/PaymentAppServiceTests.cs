using AcademiaDemo.Application.AppPayment;
using AcademiaDemo.Application.AppPayment.Input;
using AcademiaDemo.Domain.Entities;
using AcademiaDemo.Domain.Interfaces.Repository;
using AcademiaDemo.Tests.Comum;
using FluentAssertions;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AcademiaDemo.Tests.AppPayment
{
    public class PaymentAppServiceTests
    {
        private IPaymentRepository subPaymentRepository;

        public PaymentAppServiceTests()
        {
            this.subPaymentRepository = Substitute.For<IPaymentRepository>();
        }

        private PaymentAppService CreateService()
        {
            return new PaymentAppService(
                this.subPaymentRepository);
        }

        [Fact]
        public async Task InsertAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            PaymentInput input = GeneratePaymentFaker.CreatePaymentInputObject(16);

            // Act
            var result = await service.InsertAsync(input);

            // Assert
            result
                .Should()
                .BeOfType<Payment>();

            result.Id.Should().NotBe(Guid.Empty);
            result.CardNumber.Should().Be(input.CardNumber);
            result.SubDivision.Should().Be(input.SubDivision);
            result.Total.Should().Be(input.Total);

            await subPaymentRepository
                    .Received(1)
                    .InsertAsync(Arg.Any<Payment>())
                    .ConfigureAwait(false);
        }

        [Fact]
        public async Task InsertAsync_CreditCardInvalid()
        {
            // Arrange
            var service = this.CreateService();
            PaymentInput input = GeneratePaymentFaker.CreatePaymentInputObject(10);

            // Act
            Action act = () => { service.InsertAsync(input).GetAwaiter().GetResult(); };

            // Assert
            act
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Cartao de credito invalido");
            
            await subPaymentRepository
                    .DidNotReceive()
                    .InsertAsync(Arg.Any<Payment>())
                    .ConfigureAwait(false);
        }
    }
}
