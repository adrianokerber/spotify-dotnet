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
        [Obsolete("This list will be removed once the method are migrated to use the DB")]
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
    }
}