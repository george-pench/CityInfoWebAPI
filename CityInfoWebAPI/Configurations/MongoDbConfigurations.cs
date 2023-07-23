namespace CityInfoWebAPI.Configurations
{
    public class MongoDbConfigurations
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string ConnectionString
        {
            get 
            {
                return $"mongodb://{this.Host}:{this.Port}";
            }
        }
    }
}
