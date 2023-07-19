using System;

namespace CityInfoWebAPI.Dtos
{
    public record CityDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Country { get; init; }
        public double Population { get; init; }
        public string Timezone { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
