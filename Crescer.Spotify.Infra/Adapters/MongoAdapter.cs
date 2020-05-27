using Crescer.Spotify.Infra.Utils;
using MongoDB.Driver;

namespace Crescer.Spotify.Infra.Adapters
{
    public class MongoAdapter
    {
        public MongoAdapter(MongoConnectionConfigs connectionConfigs)
        {
            this.Client = new MongoClient(
                connectionConfigs.ConnectionString
            );
        }

        public MongoClient Client { get; private set; }
    }
}
