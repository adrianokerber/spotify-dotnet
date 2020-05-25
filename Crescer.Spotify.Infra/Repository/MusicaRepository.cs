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
        private IMongoCollection<MusicaOrm> collection;

        public MusicaRepository(MongoAdapter mongoAdapter)
        {
            var database = mongoAdapter.Client.GetDatabase("spotifydotnet");
            collection = database.GetCollection<MusicaOrm>("musics");
        }

        public void AtualizarMusica(string id, Musica musica)
        {
            var musicaObtida = Obter(id);
            musicaObtida?.Atualizar(musica);
            if (musicaObtida != null)
            {
                var musicaOrm = MapearDomainParaMusicaOrm(musicaObtida);
                collection.ReplaceOne(id, musicaOrm);
            }
        }

        public void DeletarMusica(string id)
        {
            collection.DeleteOne(id);
        }

        public List<Musica> ListarMusicas()
        {
            List<MusicaOrm> musicaOrmList = collection
                .Find<MusicaOrm>(_ => true).ToList();
            var musicas = musicaOrmList
                .ConvertAll(new Converter<MusicaOrm, Musica>(MapearMusicaOrmParaDomain));
            return musicas;
        }

        public List<Musica> ListarMusicas(List<string> idsMusica)
        {
            List<MusicaOrm> musicaOrmList = collection
                .Find<MusicaOrm>(x => idsMusica.Contains(x.Id.ToString())).ToList();
            var musicas = musicaOrmList
                .ConvertAll(new Converter<MusicaOrm, Musica>(MapearMusicaOrmParaDomain));
            return musicas;
        }

        public Musica Obter(string id)
        {
            var musicaOrm = collection
                .Find<MusicaOrm>(x => x.Id.Equals(ObjectId.Parse(id)))
                .FirstOrDefault();
            var musica = MapearMusicaOrmParaDomain(musicaOrm);

            return musica;
        }

        public void SalvarMusica(Musica musica)
        {
            MusicaOrm music = new MusicaOrm(musica.Nome, musica.Duracao, musica.Album?.Id);
            collection.InsertOne(music);
        }

        private Musica MapearMusicaOrmParaDomain(MusicaOrm musicaOrm)
        {
            return new Musica(musicaOrm.Nome, musicaOrm.Duracao);
        }

        private MusicaOrm MapearDomainParaMusicaOrm(Musica musica)
        {
            return new MusicaOrm(musica.Nome, musica.Duracao);
        }
    }
}