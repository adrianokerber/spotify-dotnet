using Kerber.SpotifyLibrary.Infra.Utils;
using MongoDB.Driver;

namespace Kerber.SpotifyLibrary.Infra.Adapters
{
    public class MongoAdapter
    {
        private readonly IMongoDatabase database;

        public MongoAdapter(MongoSettings connectionConfigs)
        {
            var (connectionString, databaseName) = connectionConfigs;
            database = new MongoClient(connectionString)
                .GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return database.GetCollection<T>(name);
        }
    }
}
