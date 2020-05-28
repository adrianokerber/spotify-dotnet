namespace Crescer.Spotify.Infra.Utils
{
    public class MongoSettings
    {
        public MongoSettings(string connectionString, string databaseString)
        {
            this.ConnectionString = connectionString;
            this.DatabaseString = databaseString;
        }

        public string ConnectionString { get; set; }
        public string DatabaseString { get; set; }

        public void Deconstruct(out string connection, out string database)
        {
            connection = ConnectionString;
            database = DatabaseString;
        }
    }
}
