using Crescer.Spotify.Infra.Utils;
using MongoDB.Driver;

namespace Crescer.Spotify.Infra.Adapters
{
    public class SpotifyMongoAdapter : MongoAdapter
    {
        private readonly IMongoDatabase database;

        public SpotifyMongoAdapter(MongoConnectionConfigs connectionConfigs) : base(connectionConfigs)
        {
            this.database = this.Client.GetDatabase("spotifydotnet");
        }

        public override IMongoCollection<T> GetCollection<T>(string name)
        {
            return database.GetCollection<T>(name);
        }
    }
}
