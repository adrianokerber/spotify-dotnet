using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crescer.Spotify.Infra.Entities
{
    class MusicaOrm
    {
        public MusicaOrm(string nome, double duracao)
        {
            this.Nome = nome;
            this.Duracao = duracao;
        }

        [BsonId]
        public ObjectId Id { get; set; }
        
        [BsonElement("name")]
        [BsonRequired]
        public string Nome { get; private set; }

        [BsonElement("duration")]
        [BsonRequired]
        public double Duracao { get; private set; }
    }
}
