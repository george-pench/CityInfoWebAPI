using CityInfoWebAPI.Controllers;
using CityInfoWebAPI.Dtos;
using CityInfoWebAPI.Models;
using CityInfoWebAPI.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CityInfo.UnitTests
{
    public class CitiesControllerTests
    {
        private readonly Mock<ICitiesRepository> repositoryStub = new();
        private readonly Random rand = new Random();

        [Fact]
        public async Task GetCityAsync_WithUnexistingCity_ReturnsNotFound()
        {
            // Arrange           
            this.repositoryStub.Setup(repo => repo
                .GetCityAsync(It.IsAny<Guid>()))
                .ReturnsAsync((City)null);

            var controller = new CitiesController(this.repositoryStub.Object);

            // Act
            var result = await controller.GetCityAsync(Guid.NewGuid());

            // Assert
            //Assert.IsType<NotFoundResult>(result.Result);
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetCityAsync_WithExistingCity_ReturnsExpectedCity()
        {
            // Arrange
            var expectedCity = this.CreateRandomCity();

            this.repositoryStub.Setup(repo => repo
                .GetCityAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedCity);

            var controller = new CitiesController(this.repositoryStub.Object);

            // Act
            var result = await controller.GetCityAsync(Guid.NewGuid());

            // Assert
            result.Value
                .Should()
                .BeEquivalentTo(expectedCity, options => options.ComparingByMembers<City>());
        }

        [Fact]
        public async Task GetCitiesAsync_WithExistingCities_ReturnsAllCities()
        {
            // Arrange
            var expectedCities = new[] { this.CreateRandomCity(), this.CreateRandomCity(), this.CreateRandomCity() };

            this.repositoryStub.Setup(repo => repo.GetCitiesAsync())
                .ReturnsAsync(expectedCities);

            var controller = new CitiesController(this.repositoryStub.Object);

            // Act
            var actualCities = await controller.GetCitiesAsync();

            // Assert
            actualCities
                .Should()
                .BeEquivalentTo(expectedCities, options => options.ComparingByMembers<City>());
        }

        [Fact]
        public async Task CreateCityAsync_WithCityToCreate_ReturnsCreatedCity()
        {
            // Arrange
            var cityToCreate = new CreateCityDto()
            {
                Name = Guid.NewGuid().ToString(),
                Country = Guid.NewGuid().ToString(),
                Population = rand.Next(100),
                Timezone = Guid.NewGuid().ToString()
            };
            
            var controller = new CitiesController(this.repositoryStub.Object);

            // Act
            var result = await controller.CreateCityAsync(cityToCreate);

            // Assert
            var createdCity = (result.Result as CreatedAtActionResult).Value as CityDto;

            cityToCreate
                .Should()
                .BeEquivalentTo(createdCity, options => options.ComparingByMembers<CityDto>().ExcludingMissingMembers());

            createdCity.Id.Should().NotBeEmpty();
            createdCity.CreatedDate.Should().BeCloseTo(DateTimeOffset.UtcNow, 1000);
        }

        [Fact]
        public async Task UpdateCityAsync_WithExistingCity_ReturnsNoContent()
        {
            // Arrange
            var existingCity = this.CreateRandomCity();

            this.repositoryStub.Setup(repo => repo
                .GetCityAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingCity);

            var cityId = existingCity.Id;
            var cityToUpdate = new UpdateCityDto() 
            {
                Name = Guid.NewGuid().ToString(), 
                Country = Guid.NewGuid().ToString(),
                Population = existingCity.Population + 1,
                Timezone = Guid.NewGuid().ToString()
            };

            var controller = new CitiesController(this.repositoryStub.Object);

            // Act
            var result = await controller.UpdateCityAsync(cityId, cityToUpdate);

            // Assert
            result.Should().BeOfType<NoContentResult>();          
        }

        [Fact]
        public async Task DeleteCityAsync_WithExistingCity_ReturnsNoContent()
        {
            // Arrange
            var existingCity = this.CreateRandomCity();

            this.repositoryStub.Setup(repo => repo
                .GetCityAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingCity);

            var controller = new CitiesController(this.repositoryStub.Object);

            // Act
            var result = await controller.DeleteCityAsync(existingCity.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        private City CreateRandomCity() => new()
        {
            Id = Guid.NewGuid(),
            Name = Guid.NewGuid().ToString(),
            Country = Guid.NewGuid().ToString(),
            Population = rand.Next(100),
            Timezone = Guid.NewGuid().ToString(),
            CreatedDate = DateTimeOffset.UtcNow
        };
    }
}
