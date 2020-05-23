using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Crescer.Spotify.Infra.Entities
{
    class AlbumEntity
    {
        public AlbumEntity(string nome)
        {
            Nome = nome;
            IdsMusica = new List<ObjectId>();
        }

        public AlbumEntity(string nome, List<ObjectId> idsMusica)
        {
            Nome = nome;
            IdsMusica = idsMusica;
        }

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        [BsonRequired]
        public string Nome { get; private set; }

        [BsonElement("musicIds")]
        public List<ObjectId> IdsMusica { get; private set; }
    }
}
