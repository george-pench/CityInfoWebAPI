using CityInfoWebAPI.Dtos;
using CityInfoWebAPI.Extensions;
using CityInfoWebAPI.Models;
using CityInfoWebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<CityDto> GetCities() 
        {
            var cities = this.repository.GetCities().Select(city => city.AsDto());

            return cities;
        }

        // GET /cities/{id}
        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(Guid id) 
        {
            var city = this.repository.GetCity(id);            

            if (city is null)
            {
                return this.NotFound();
            }

            return city.AsDto();
        }

        // POST /cities
        [HttpPost]
        public ActionResult<CreateCityDto> CreateCity(CreateCityDto cityDto)
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

            this.repository.CreateCity(city);

            return CreatedAtAction(nameof(this.GetCity), new { id = city.Id }, city.AsDto());
        }

        // PUT /cities/{id}
        [HttpPut]
        public ActionResult UpdateCity(Guid id, UpdateCityDto cityDto)
        {
            var existingCity = this.repository.GetCity(id);

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

            this.repository.UpdateCity(updatedCity);

            return this.NoContent();
        }

        // DELETE /cities/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCity(Guid id) 
        {
            var existingCity = this.repository.GetCity(id);

            if (existingCity is null)
            {
                return this.NotFound();
            }

            this.repository.DeleteCity(id);

            return this.NoContent();
        }
    }
}
