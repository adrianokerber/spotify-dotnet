using Kerber.SpotifyLibrary.Infra.Configs;
using Microsoft.Extensions.Configuration;
using System;

namespace Kerber.SpotifyLibrary.WebApi.Configs
{
    public static class MongoConfigurationLoader
    {
        /*
         * [databaseConfigKey] - Should be the name of the object in appsettings.json inside [DatabaseConfigs]
         */
        public static MongoSettings Load(IConfiguration configuration, string databaseConfigKey)
        {
            var mongoConfigs = configuration.GetSection("DatabaseConfigs").GetSection(databaseConfigKey);
            var mongoConnection = mongoConfigs["ConnectionString"] ?? throw new ArgumentNullException("configuration.DatabaseConfigs.ConnectionString");
            var mongoDatabase = mongoConfigs["DatabaseString"] ?? throw new ArgumentNullException("configuration.DatabaseConfigs.DatabaseString");

            return new MongoSettings(mongoConnection, mongoDatabase);
        }
    }
}
