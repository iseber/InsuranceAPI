using System.Collections.Generic;
using System.Threading.Tasks;
using Insurance.Api.Controllers;
using Insurance.Api.Models;
using Insurance.Api.Services;
using Insurance.Tests.Configuration;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Insurance.Tests
{
    [Collection("Controller collection")]
    public class OrderInsuranceTests
    {
        private readonly ControllerTestFixture _fixture;

        public OrderInsuranceTests(ControllerTestFixture fixture)
        {
            _fixture = fixture;
        }
        
        // [Fact]
        // public async Task CalculateInsurance_GivenOrderWithMultipleProducts_ShouldAddThousandAndFiveHundredEurosToOrderInsuranceCost()
        // {
        //     const float expectedInsuranceValue = 1500;
        //     
        //     var businessRules = new BusinessRules(MockHelper.AppSettings());
        //     var sut = new HomeController(businessRules);
        //     var insuranceRequestModel = new OrderInsuranceRequestModel {ProductIds = new List<int> {1, 2}};
        //     var result = await sut.CalculateInsurance(insuranceRequestModel) as ObjectResult;
        //
        //     var model = result.Value as OrderInsuranceResponseModel;
        //     Assert.Equal(
        //         expected: expectedInsuranceValue,
        //         actual: model.SumOfOrderInsurance
        //     );
        // }
    }
}