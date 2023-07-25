using CityInfoWebAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityInfoWebAPI.Repositories
{
    public class MongoDbCitiesRepository : ICitiesRepository
    {
        private const string databaseName = "CityInfo";
        private const string collectionName = "cities";

        private readonly IMongoCollection<City> citiesCollection;
        private readonly FilterDefinitionBuilder<City> filterDefinitionBuilder = Builders<City>.Filter;

        public MongoDbCitiesRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            this.citiesCollection = database.GetCollection<City>(collectionName);
        }

        public async Task<City> GetCityAsync(Guid id)
        {
            var filter = this.filterDefinitionBuilder.Eq(city => city.Id, id);
            return await this.citiesCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<City>> GetCitiesAsync() 
            => await this.citiesCollection.Find(new BsonDocument()).ToListAsync();       

        public async Task CreateCityAsync(City city) 
            => await this.citiesCollection.InsertOneAsync(city);

        public async Task UpdateCityAsync(City city)
        {
            var filter = this.filterDefinitionBuilder.Eq(existingCity => existingCity.Id, city.Id);
            await this.citiesCollection.ReplaceOneAsync(filter, city);
        }

        public async Task DeleteCityAsync(Guid id)
        {
            var filter = this.filterDefinitionBuilder.Eq(city => city.Id, id);
            await this.citiesCollection.DeleteOneAsync(filter);
        }
    }
}
