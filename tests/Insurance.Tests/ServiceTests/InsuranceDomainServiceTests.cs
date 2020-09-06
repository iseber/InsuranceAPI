using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FluentAssertions;
using Insurance.Api.Models;
using Insurance.Api.Repositories;
using Insurance.Api.Services;
using Moq;
using Xunit;

namespace Insurance.Tests.ServiceTests
{
    public class InsuranceDomainServiceTests
    {
        [Fact]
        public async Task GetInsurance_GivenProductBelow500_ShouldReturn0InsuranceCost()
        {
            var product = new
            {
                id = 1,
                name = "Test Product",
                productTypeId = 1,
                salesPrice = 250
            };

            var expectdProduct = new ProductResponseModel
            {
                ProductTypeId = 1,
                ProductTypeHasInsurance = true,
                ProductTypeName = "Test",
                SalesPrice = 250
            };
           
            var businessRuleMock = new Mock<IBusinessRules>(MockBehavior.Strict);
            businessRuleMock.Setup(x => x.GetProductTypeAndSalesPrice(product.productTypeId))
                .ReturnsAsync(expectdProduct);

            var insuranceRepositoryMock = new Mock<IInsuranceRepository>(MockBehavior.Strict);
            insuranceRepositoryMock.Setup(x => x.GetInsuranceByProductPrice(expectdProduct.SalesPrice)).ReturnsAsync(0);
            insuranceRepositoryMock.Setup(x => x.GetSurchargeByProductTypeId(expectdProduct.ProductTypeId)).ReturnsAsync(0);
           
            var insuranceDomainService = new InsuranceDomainService(insuranceRepositoryMock.Object, businessRuleMock.Object);
            var insurance = await insuranceDomainService.GetInsurance(product.id);

            insurance.ProductId.Should().Be(product.id);
            insurance.InsuranceValue.Should().Be(0);
        }

        [Fact]
        public async Task GetOrderSurcharge_GivenProductsHasOrderSurcharges_ShouldReturnSurchargetotalCost()
        {
            var product1 = new
            {
                id = 1,
                name = "Test1 Product",
                productTypeId = 1,
                salesPrice = 250
            };
            
            var product2 = new
            {
                id = 2,
                name = "Test2 Product",
                productTypeId = 4,
                salesPrice = 750
            };

            var productTypes = new[] {product1.id, product2.id};
            
            var businessRuleMock = new Mock<IBusinessRules>(MockBehavior.Strict);

            var insuranceRepositoryMock = new Mock<IInsuranceRepository>(MockBehavior.Strict);
            insuranceRepositoryMock.Setup(x => x.GetOrderSurchargeByProductTypeIds(new []{product1.id, product2.id})).ReturnsAsync(500);
           
            var insuranceDomainService = new InsuranceDomainService(insuranceRepositoryMock.Object, businessRuleMock.Object);
            var surcharge = await insuranceDomainService.GetOrderSurcharge(productTypes);

            surcharge.Should().Be(500);
        }
    }
}