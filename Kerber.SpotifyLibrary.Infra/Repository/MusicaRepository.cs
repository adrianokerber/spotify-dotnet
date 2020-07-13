using Kerber.SpotifyLibrary.Domain.Contratos;
using Kerber.SpotifyLibrary.Domain.Entidades;
using Kerber.SpotifyLibrary.Infra.Adapters;
using Kerber.SpotifyLibrary.Infra.Entities;
using Kerber.SpotifyLibrary.Infra.Mappers;
using Kerber.SpotifyLibrary.Infra.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kerber.SpotifyLibrary.Infra.Repository
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
                var musicaOrm = musicaObtida.MapearDomainParaOrm();
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
            return musicaOrmList.MapearCollectionOrmParaCollectionDomain();
        }

        public List<Musica> ListarMusicas(List<string> idsMusica)
        {
            var objectIdsDeMusicas = idsMusica.ConvertAll(
                new Converter<string, ObjectId>(MongoStringExtensions.ToObjectId)
                );
            
            var encontrarTodasAsMusicasDoArray = Builders<MusicaOrm>.Filter.In(x => x.Id, objectIdsDeMusicas);

            List<MusicaOrm> musicaOrmList = collection
                .Find<MusicaOrm>(encontrarTodasAsMusicasDoArray).ToList();
            return musicaOrmList.MapearCollectionOrmParaCollectionDomain();
        }

        public List<Musica> ListarMusicasPorNome(List<string> nomesDeMusicas)
        {
            var encontrarTodasAsMusicasPorNome = Builders<MusicaOrm>.Filter.In(x => x.Nome, nomesDeMusicas);

            List<MusicaOrm> musicaOrmList = collection
                .Find<MusicaOrm>(encontrarTodasAsMusicasPorNome).ToList();
            return musicaOrmList.MapearCollectionOrmParaCollectionDomain();
        }

        public Musica Obter(string id)
        {
            var objectId = id.ToObjectId();
            var musicaOrm = collection
                .Find<MusicaOrm>(x => x.Id.Equals(objectId))
                .FirstOrDefault();

            if (musicaOrm == null)
                return null;

            var musica = musicaOrm.MapearOrmParaDomain();
            return musica;
        }

        public Musica SalvarMusica(Musica musica)
        {
            MusicaOrm musicaOrm = musica.MapearDomainParaOrm();
            collection.InsertOne(musicaOrm);
            return musicaOrm.MapearOrmParaDomain();
        }

        public List<Musica> SalvarMusicas(List<Musica> musicas)
        {
            List<MusicaOrm> listDeMusicaOrm = musicas.MapearCollectionDomainParaCollectionOrm();
            collection.InsertMany(listDeMusicaOrm);
            return listDeMusicaOrm.MapearCollectionOrmParaCollectionDomain();
        }
    }
}