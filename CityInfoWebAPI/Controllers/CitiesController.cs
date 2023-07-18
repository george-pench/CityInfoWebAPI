using CityInfoWebAPI.Models;
using CityInfoWebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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

        // GET /items
        [HttpGet]
        public IEnumerable<City> GetCities() 
        {
            var cities = this.repository.GetCities();
            return cities;
        }
            
        // GET /items/{id}
        [HttpGet("{id}")]
        public ActionResult<City> GetCity(Guid id) 
        {
            var city = this.repository.GetCity(id);

            if (city is null)
            {
                return this.NotFound();
            }

            return city;
        }
    }
}
