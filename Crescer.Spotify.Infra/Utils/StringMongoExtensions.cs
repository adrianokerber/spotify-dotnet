using MongoDB.Bson;
using System;

namespace Crescer.Spotify.Infra.Utils
{
    public static class StringMongoExtensions
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
