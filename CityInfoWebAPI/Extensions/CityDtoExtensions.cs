using CityInfoWebAPI.Dtos;
using CityInfoWebAPI.Models;

namespace CityInfoWebAPI.Extensions
{
    public static class CityDtoExtensions
    {
        public static CityDto AsDto(this City city) 
        {
            return new CityDto
            {
                Id = city.Id,
                Name = city.Name,
                Country = city.Country,
                Population = city.Population,
                Timezone = city.Timezone,
                CreatedDate = city.CreatedDate
            };
        }
    }
}
