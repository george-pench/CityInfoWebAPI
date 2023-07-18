using CityInfoWebAPI.Models;
using System;
using System.Collections.Generic;

namespace CityInfoWebAPI.Repositories
{
    public interface ICitiesRepository
    {
        City GetCity(Guid id);

        IEnumerable<City> GetCities();
    }
}
