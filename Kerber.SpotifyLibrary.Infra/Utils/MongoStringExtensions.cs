using MongoDB.Bson;
using System;

namespace Kerber.SpotifyLibrary.Infra.Utils
{
    public static class MongoStringExtensions
    {
        /*
         * It converts the string to ObjectId if possible, otherwise returns the default value
         */
        public static ObjectId ToObjectId(this String id)
        {
            if (ObjectId.TryParse(id, out ObjectId parsedId))
                return parsedId;
            else
                return default;
        }
    }
}
