using System;

namespace CityInfoWebAPI.Models
{
    public record City
    {
        public Guid Id { get; init; }
        
        public string Name { get; init; }

        public string Country { get; init; }

        public double Population { get; init; }

        public string Timezone { get; init; }

        public DateTimeOffset CreatedDate { get; init; }
    }
}
