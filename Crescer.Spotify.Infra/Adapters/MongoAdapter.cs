using MongoDB.Driver;

namespace Crescer.Spotify.Infra.Adapters
{
    public class MongoAdapter
    {
        private MongoClient client = new MongoClient(
            "mongodb+srv://spotifydotnetUser:2qNECVRjmAQHYsfx@clusterzero-09qhx.mongodb.net/test?retryWrites=true&w=majority"
        );

        public MongoClient Client {
            get { return this.client; }
            private set { }
        }
    }
}
