namespace Kerber.SpotifyLibrary.Infra.Configs
{
    public class MongoSettings
    {
        public MongoSettings(string connectionString, string databaseString)
        {
            ConnectionString = connectionString;
            DatabaseString = databaseString;
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
