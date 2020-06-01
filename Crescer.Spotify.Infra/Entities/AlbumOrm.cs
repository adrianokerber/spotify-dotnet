using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Crescer.Spotify.Infra.Entities
{
    public class AlbumOrm
    {
        public AlbumOrm(string nome, ObjectId id = default)
        {
            this.Nome = nome;
            this.ListaDeIdsDeMusica = new List<ObjectId>();
            this.Id = id;
        }

        public AlbumOrm(string nome, List<ObjectId> listaDeIdsDeMusica, ObjectId id = default)
        {
            this.Nome = nome;
            this.ListaDeIdsDeMusica = listaDeIdsDeMusica;
            this.Id = id;
        }

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        [BsonRequired]
        public string Nome { get; private set; }

        [BsonElement("musicIds")]
        public List<ObjectId> ListaDeIdsDeMusica { get; private set; }
    }
}
