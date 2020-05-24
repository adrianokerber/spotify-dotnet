using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Crescer.Spotify.Infra.Entities
{
    class AlbumOrm
    {
        public AlbumOrm(string nome)
        {
            Nome = nome;
            ListaDeIdsDeMusica = new List<ObjectId>();
        }

        public AlbumOrm(string nome, List<ObjectId> listaDeIdsDeMusica)
        {
            Nome = nome;
            ListaDeIdsDeMusica = listaDeIdsDeMusica;
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
