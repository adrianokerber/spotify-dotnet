﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Kerber.SpotifyLibrary.Infra.Entities
{
    public class MusicaOrm
    {
        public MusicaOrm(string nome, double duracao, ObjectId id = default)
        {
            this.Nome = nome;
            this.Duracao = duracao;
            this.Id = id;
        }

        [BsonId]
        public ObjectId Id { get; private set; }
        
        [BsonElement("name")]
        [BsonRequired]
        public string Nome { get; private set; }

        [BsonElement("duration")]
        [BsonRequired]
        public double Duracao { get; private set; }
    }
}
