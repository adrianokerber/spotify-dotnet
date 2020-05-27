using Crescer.Spotify.Infra.Utils;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Crescer.Spotify.Infra.Adapters
{
    public abstract class MongoAdapter
    {
        public MongoAdapter(MongoConnectionConfigs connectionConfigs)
        {
            this.Client = new MongoClient(
                connectionConfigs.ConnectionString
            );
        }

        protected MongoClient Client { get; private set; }

        public abstract IMongoCollection<T> GetCollection<T>(string name);
    }
}
