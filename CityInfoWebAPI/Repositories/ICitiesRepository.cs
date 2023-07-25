using CityInfoWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityInfoWebAPI.Repositories
{
    public interface ICitiesRepository
    {
        Task<City> GetCityAsync(Guid id);
        Task<IEnumerable<City>> GetCitiesAsync();
        Task CreateCityAsync(City city);
        Task UpdateCityAsync(City city);
        Task DeleteCityAsync(Guid id);
    }
}
