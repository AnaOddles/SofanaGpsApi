using System;
using SofanaGPSApi.Models;
using SofanaGPSApi.Controllers;
using SofanaGPSApi.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Collections.Generic;
using SofanaGPSApi_Testing;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SofanaGPApi_Testing.System.Controllers
{
    /// <summary>
    /// Test class for LocationController functionality
    /// </summary>
    public class TestLocationController
    {
        /// <summary>
        /// Integration test for 200 status Get() endpoint
        /// </summary>
        /// <returns>async Task</returns>
        [Fact]
        public async Task Get_ShouldReturn200Status()
        {
            // Arrange
            var locationService = new Mock<ILocationService>();
            locationService.Setup(_=> _.Get()).ReturnsAsync(LocationMockData.GetLocations());
            var sut = new LocationsController(locationService.Object);

            // Act
            var result = (OkObjectResult)await sut.Get();

            // Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Integration test for 204 status Get() endpoint
        /// </summary>
        /// <returns>async Task</returns>
        [Fact]
        public async Task Get_ShouldReturn204NoContentStatus()
        {
            // Arrange
            var locationService = new Mock<ILocationService>();
            locationService.Setup(_ => _.Get()).ReturnsAsync(LocationMockData.GetEmptyLocations());
            var sut = new LocationsController(locationService.Object);

            // Act
            var result = (NoContentResult)await sut.Get();

            // Assert
            result.StatusCode.Should().Be(204);
            locationService.Verify(_ => _.Get(), Times.Exactly(1));

        }

        /// <summary>
        /// Integration test for 200 status GetLast() endpoint
        /// </summary>
        /// <returns>async Task</returns>
        [Fact]
        public async Task GetLast_ShouldReturn200Status()
        {
            // Arrange
            var locationService = new Mock<ILocationService>();
            locationService.Setup(_ => _.GetLastCoordinates()).ReturnsAsync(LocationMockData.GetLocations());
            var sut = new LocationsController(locationService.Object);

            // Act
            var result = (OkObjectResult)await sut.GetLast();

            // Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Integation test for 200 status GetAllWithCartId(int cartId) endpoint
        /// </summary
        /// <param name="cartId"></param>
        /// <returns>async Task</returns>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task GetAllWithCartId_ShouldReturn200Status(int cartId)
        {
            // Arrange
            var locationService = new Mock<ILocationService>();
            locationService.Setup(_ => _.GetAllWithCartId(cartId)).ReturnsAsync(LocationMockData.GetAllWithCartId(cartId));
            var sut = new LocationsController(locationService.Object);

            // Act
            var result = (OkObjectResult)await sut.GetAllWithCartId(cartId);

            // Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Integration test for 200 status GetWithCartId(string id) endpoint
        /// </summary>
        /// <param name="id"></param>
        /// <returns>async Task</returns>
        [Theory]
        [InlineData("624f77b47f7eee1c98bee1cf")]
        [InlineData("624e2e9ecc1c67f0c0cef4cd")]
        public async Task GetWithCartId_ShouldReturn200Status(string id)
        {
            // Arrange
            var locationService = new Mock<ILocationService>();
            locationService.Setup(_ => _.Get(id)).ReturnsAsync(LocationMockData.Get(id));
            var sut = new LocationsController(locationService.Object);

            // Act
            var result = (OkObjectResult)await sut.Get(id);

            // Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Integration test for Create() endpoint
        /// </summary>
        /// <returns>async Task</returns>
        [Fact]
        public async Task Create_ShouldCall_ILocationService_Create_AtleastOnce()
        {
            // Arrange
            var locationService = new Mock<ILocationService>();
            var newLocation = LocationMockData.NewLocation();
            var sut = new LocationsController(locationService.Object);

            // Act
            var result = await sut.Create(newLocation);

            // Assert
            locationService.Verify(_ => _.Create(newLocation), Times.Exactly(1));
        }
    }
}
