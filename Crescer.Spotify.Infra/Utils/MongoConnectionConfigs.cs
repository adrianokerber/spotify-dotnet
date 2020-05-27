using System;
using System.Collections.Generic;
using System.Text;

namespace Crescer.Spotify.Infra.Utils
{
    public class MongoConnectionConfigs
    {
        public MongoConnectionConfigs(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }
    }
}
