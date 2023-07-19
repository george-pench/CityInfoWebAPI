using System.ComponentModel.DataAnnotations;

namespace CityInfoWebAPI.Dtos
{
    public record UpdateCityDto
    {
        [Required]
        public string Name { get; init; }

        [Required]
        public string Country { get; init; }

        [Required]
        [Range(0, 100)]
        public double Population { get; init; }

        public string Timezone { get; init; }
    }
}
