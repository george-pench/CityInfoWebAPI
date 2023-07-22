using CityInfoWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CityInfoWebAPI.Repositories
{
    public class CitiesRepository : ICitiesRepository
    {
        private readonly List<City> cities = new()
        {
            new City { Id = Guid.NewGuid(), Name = "New York", Country = "USA", Population = 8.468, Timezone = "GMT-4", CreatedDate = DateTimeOffset.UtcNow },
            new City { Id = Guid.NewGuid(), Name = "Tokyo", Country = "Japan", Population = 13.96, Timezone = "GMT+9", CreatedDate = DateTimeOffset.UtcNow },
            new City { Id = Guid.NewGuid(), Name = "Moscow", Country = "Russia", Population = 11.98, Timezone = "GMT+3", CreatedDate = DateTimeOffset.UtcNow },
            new City { Id = Guid.NewGuid(), Name = "Beijing", Country = "China", Population = 21.54, Timezone = "GMT+8", CreatedDate = DateTimeOffset.UtcNow },
            new City { Id = Guid.NewGuid(), Name = "Rio de Janeiro", Country = "Brazil", Population = 6.748, Timezone = "GMT-3", CreatedDate = DateTimeOffset.UtcNow },
            new City { Id = Guid.NewGuid(), Name = "Sofia", Country = "Bulgaria", Population = 1.236, Timezone = "GMT+3", CreatedDate = DateTimeOffset.UtcNow }
        };

        public City GetCity(Guid id) => this.cities.Where(city => city.Id == id).SingleOrDefault();

        public IEnumerable<City> GetCities() => this.cities;

        public void CreateCity(City city) => this.cities.Add(city);

        public void UpdateCity(City city)
        {
            var index = this.cities.FindIndex(existingCity => existingCity.Id == city.Id);
            this.cities[index] = city;
        }

        public void DeleteCity(Guid id)
        {
            var index = this.cities.FindIndex(existingCity => existingCity.Id == id);
            this.cities.RemoveAt(index);
        }
    }
}
