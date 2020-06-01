using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Infra.Adapters;
using Crescer.Spotify.Infra.Entities;
using static Crescer.Spotify.Infra.Mappers.MusicaOrmMapper;
using Crescer.Spotify.Infra.Utils;
using MongoDB.Bson;
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
            collection = mongoAdapter.GetCollection<MusicaOrm>("musics");
        }

        public void AtualizarMusica(string id, Musica musica)
        {
            var musicaObtida = Obter(id);
            musicaObtida?.Atualizar(musica);
            if (musicaObtida != null)
            {
                var musicaOrm = MapearDomainParaOrm(musicaObtida);
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
                .ConvertAll(new Converter<MusicaOrm, Musica>(MapearOrmParaDomain));
            return musicas;
        }

        public List<Musica> ListarMusicas(List<string> idsMusica)
        {
            var objectIdsDeMusicas = idsMusica.ConvertAll(
                new Converter<string, ObjectId>(MongoStringExtensions.ToObjectId)
                );
            
            var encontrarTodasAsMusicasDoArray = Builders<MusicaOrm>.Filter.In(x => x.Id, objectIdsDeMusicas);

            List<MusicaOrm> musicaOrmList = collection
                .Find<MusicaOrm>(encontrarTodasAsMusicasDoArray).ToList();
            var musicas = musicaOrmList
                .ConvertAll(new Converter<MusicaOrm, Musica>(MapearOrmParaDomain));
            return musicas;
        }

        public List<Musica> ListarMusicasPorNome(List<string> nomesDeMusicas)
        {
            var encontrarTodasAsMusicasPorNome = Builders<MusicaOrm>.Filter.In(x => x.Nome, nomesDeMusicas);

            List<MusicaOrm> musicaOrmList = collection
                .Find<MusicaOrm>(encontrarTodasAsMusicasPorNome).ToList();
            var musicas = musicaOrmList
                .ConvertAll(new Converter<MusicaOrm, Musica>(MapearOrmParaDomain));
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

            var musica = MapearOrmParaDomain(musicaOrm);
            return musica;
        }

        public Musica SalvarMusica(Musica musica)
        {
            MusicaOrm musicaOrm = MapearDomainParaOrm(musica);
            collection.InsertOne(musicaOrm);
            return MapearOrmParaDomain(musicaOrm);
        }

        public List<Musica> SalvarMusicas(List<Musica> musicas)
        {
            List<MusicaOrm> musicaOrms = MapearCollectionDomainParaCollectionOrm(musicas);
            collection.InsertMany(musicaOrms);
            return MapearCollectionOrmParaCollectionDomain(musicaOrms);
        }
    }
}