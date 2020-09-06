using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insurance.Api.Controllers;
using Insurance.Api.Models;
using Insurance.Api.Repositories;
using Insurance.Api.Services;
using Insurance.Tests.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Xunit;

namespace Insurance.Tests
{
    [Collection("Controller collection")]

    public class InsuranceTests
    {
        private readonly ControllerTestFixture _fixture;

        public InsuranceTests(ControllerTestFixture fixture)
        {
            _fixture = fixture;
        }

        // [Fact]
        // public async Task CalculateInsurance_GivenSalesPriceBetween500And2000Euros_ShouldAddThousandEurosToInsuranceCost()
        // {
        //     const float expectedInsuranceValue = 1000;
        //     
        //     var businessRules = new BusinessRules(MockHelper.AppSettings());
        //     var insuranceRepository = new InsuranceRepository();
        //     var insuranceDomainService = new InsuranceDomainService(insuranceRepository, businessRules);
        //     var sut = new HomeController(insuranceDomainService);
        //     
        //     var insuranceRequestModel = new InsuranceRequestModel {ProductId = 1};
        //     var result = await sut.CalculateInsurance(insuranceRequestModel) as ObjectResult;
        //
        //     var model = result.Value as InsuranceResponseModel;
        //     Assert.Equal(
        //         expected: expectedInsuranceValue,
        //         actual: model.InsuranceValue
        //     );
        // }
        //
        // [Fact]
        // public async Task CalculateInsurance_GivenSalesPriceBelow500AndProductTypeLaptop_ShouldAddFiveHundredEurosToInsuranceCost()
        // {
        //     const float expectedInsuranceValue = 500;
        //     
        //     var businessRules = new BusinessRules(MockHelper.AppSettings());
        //     var sut = new HomeController(businessRules);
        //     var insuranceRequestModel = new InsuranceRequestModel {ProductId = 2};
        //     var result = await sut.CalculateInsurance(insuranceRequestModel) as ObjectResult;
        //
        //     var model = result.Value as InsuranceResponseModel;
        //     Assert.Equal(
        //         expected: expectedInsuranceValue,
        //         actual: model.InsuranceValue
        //     );
        // }
    }
}