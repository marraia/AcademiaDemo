using AcademiaDemo.Application.AppPayment.Input;
using AcademiaDemo.Application.AppPayment.Output;
using AcademiaDemo.Application.AppSale;
using AcademiaDemo.Application.AppSale.Input;
using AcademiaDemo.Application.AppStock.Input;
using AcademiaDemo.Application.AppStock.Output;
using AcademiaDemo.Domain.Interfaces.Repository;
using AcademiaDemo.Infrastructure.Repository.Gateway.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AcademiaDemo.Tests.AppSale
{
    public class SaleAppServiceTests
    {
        private IConfiguration subConfiguration;
        private IHttpClientGateway<PaymentInput, PaymentOutput> subHttpClientGatewayPaymentInputPaymentOutput;
        private IHttpClientGateway<StockInput, StockOutput> subHttpClientGatewayStockInputStockOutput;
        private IItemRepository subItemRepository;
        private ISaleRepository subSaleRepository;

        public SaleAppServiceTests()
        {
            this.subConfiguration = Substitute.For<IConfiguration>();
            this.subHttpClientGatewayPaymentInputPaymentOutput = Substitute.For<IHttpClientGateway<PaymentInput, PaymentOutput>>();
            this.subHttpClientGatewayStockInputStockOutput = Substitute.For<IHttpClientGateway<StockInput, StockOutput>>();
            this.subItemRepository = Substitute.For<IItemRepository>();
            this.subSaleRepository = Substitute.For<ISaleRepository>();
        }

        private SaleAppService CreateService()
        {
            return new SaleAppService(
                this.subConfiguration,
                this.subHttpClientGatewayPaymentInputPaymentOutput,
                this.subHttpClientGatewayStockInputStockOutput,
                this.subItemRepository,
                this.subSaleRepository);
        }

        [Fact]
        public void InsertAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            SaleInput input = new SaleInput();
            input.Itens = new List<Guid>();

            // Act
            Action act = () => { service.InsertAsync(input).GetAwaiter().GetResult(); };

            // Assert
            act
            .Should()
            .Throw<Exception>()
            .WithMessage("Para a venda, e necessario pelo menos informar um item");
        }
    }
}
