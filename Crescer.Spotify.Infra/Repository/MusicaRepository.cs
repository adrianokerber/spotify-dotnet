using System;
using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Infra.Adapters;
using Crescer.Spotify.Infra.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Crescer.Spotify.Infra.Repository
{
    public class MusicaRepository : IMusicaRepository
    {
        private static List<Musica> musicas = new List<Musica>();
        private IMongoCollection<MusicaOrm> collection;

        public MusicaRepository(MongoAdapter mongoAdapter)
        {
            var database = mongoAdapter.Client.GetDatabase("spotifydotnet");
            collection = database.GetCollection<MusicaOrm>("music");
        }

        public void AtualizarMusica(string id, Musica musica)
        {
            var musicaObtida = Obter(id);
            musicaObtida?.Atualizar(musica);
        }

        public void DeletarMusica(string id)
        {
            var musica = this.Obter(id);
            musicas.Remove(musica);
        }

        public List<Musica> ListarMusicas()
        {
            return musicas;
        }

        public List<Musica> ListarMusicas(List<string> idsMusica)
        {
            return musicas.Where(x => idsMusica.Contains(x.Id.ToString())).ToList();
        }

        public Musica Obter(string id)
        {
            var musicaOrm = collection
                .Find<MusicaOrm>(x => x.Id.Equals(ObjectId.Parse(id)))
                .FirstOrDefault();
            var musica = MapearEntityParaDomain(musicaOrm);

            return musica;
        }

        public void SalvarMusica(Musica musica)
        {
            var music = new MusicaOrm(musica.Nome, musica.Duracao);
            collection.InsertOne(music);
        }

        private Musica MapearEntityParaDomain(MusicaOrm musicaOrm)
        {
            // TODO: Usar AutoMapper para converter
            throw new NotImplementedException();
        }
    }
}