using CityInfoWebAPI.Dtos;
using CityInfoWebAPI.Extensions;
using CityInfoWebAPI.Models;
using CityInfoWebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoWebAPI.Controllers
{
    [ApiController]
    [Route("/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesRepository repository;

        public CitiesController(ICitiesRepository repository)
        {
            this.repository = repository;
        }

        // GET /cities
        [HttpGet]
        public async Task<IEnumerable<CityDto>> GetCitiesAsync() 
        {
            var cities = (await this.repository.GetCitiesAsync())
                .Select(city => city.AsDto());

            return cities;
        }

        // GET /cities/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> GetCityAsync(Guid id) 
        {
            var city = await this.repository.GetCityAsync(id);            

            if (city is null)
            {
                return this.NotFound();
            }

            return city.AsDto();
        }

        // POST /cities
        [HttpPost]
        public async Task<ActionResult<CreateCityDto>> CreateCityAsync(CreateCityDto cityDto)
        {
            City city = new City()
            {
                Id = Guid.NewGuid(),
                Name = cityDto.Name,
                Country = cityDto.Country,
                Population = cityDto.Population,
                Timezone = cityDto.Timezone,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await this.repository.CreateCityAsync(city);

            return CreatedAtAction(nameof(this.GetCityAsync), new { id = city.Id }, city.AsDto());
        }

        // PUT /cities/{id}
        [HttpPut]
        public async Task<ActionResult> UpdateCityAsync(Guid id, UpdateCityDto cityDto)
        {
            var existingCity = await this.repository.GetCityAsync(id);

            if (existingCity is null)
            {
                return this.NotFound();
            }

            City updatedCity = existingCity with
            {
                Name = cityDto.Name,
                Country = cityDto.Country,
                Population = cityDto.Population,
                Timezone = cityDto.Timezone,
            };

            await this.repository.UpdateCityAsync(updatedCity);

            return this.NoContent();
        }

        // DELETE /cities/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCityAsync(Guid id) 
        {
            var existingCity = await this.repository.GetCityAsync(id);

            if (existingCity is null)
            {
                return this.NotFound();
            }

            await this.repository.DeleteCityAsync(id);

            return this.NoContent();
        }
    }
}
