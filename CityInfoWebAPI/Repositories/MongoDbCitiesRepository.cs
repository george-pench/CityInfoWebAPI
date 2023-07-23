using CityInfoWebAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

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
            citiesCollection = database.GetCollection<City>(collectionName);
        }

        public void CreateCity(City city) => this.citiesCollection.InsertOne(city);

        public void DeleteCity(Guid id)
        {
            var filter = this.filterDefinitionBuilder.Eq(city => city.Id, id);
            this.citiesCollection.DeleteOne(filter);
        }

        public IEnumerable<City> GetCities() => this.citiesCollection.Find(new BsonDocument()).ToList();

        public City GetCity(Guid id)
        {
            var filter = this.filterDefinitionBuilder.Eq(city => city.Id, id);
            return this.citiesCollection.Find(filter).SingleOrDefault();
        }

        public void UpdateCity(City city)
        {
            var filter = this.filterDefinitionBuilder.Eq(existingCity => existingCity.Id, city.Id);
            this.citiesCollection.ReplaceOne(filter, city);
        }
    }
}
