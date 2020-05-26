using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Infra.Adapters;
using Crescer.Spotify.Infra.Entities;
using Crescer.Spotify.Infra.Utils;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

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
                var objectId = id.ToObjectId();
                collection.ReplaceOne(x => x.Id.Equals(objectId), musicaOrm);
            }
        }

        public void DeletarMusica(string id)
        {
            var objectId = id.ToObjectId();
            collection.DeleteOne(x => x.Id.Equals(objectId));
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
            var objectId = id.ToObjectId();
            var musicaOrm = collection
                .Find<MusicaOrm>(x => x.Id.Equals(objectId))
                .FirstOrDefault();

            if (musicaOrm == null)
                return null;

            var musica = MapearMusicaOrmParaDomain(musicaOrm);
            return musica;
        }

        public void SalvarMusica(Musica musica)
        {
            MusicaOrm musicaOrm = MapearDomainParaMusicaOrm(musica);
            collection.InsertOne(musicaOrm);
        }

        private Musica MapearMusicaOrmParaDomain(MusicaOrm musicaOrm)
        {
            return new Musica(musicaOrm.Nome, musicaOrm.Duracao, id: musicaOrm.Id.ToString());
        }

        private MusicaOrm MapearDomainParaMusicaOrm(Musica musica)
        {
            return new MusicaOrm(musica.Nome, musica.Duracao, id: musica.Id.ToObjectId());
        }
    }
}