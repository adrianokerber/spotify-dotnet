using System;
using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Infra.Adapters;
using Crescer.Spotify.Infra.Entities;
using Crescer.Spotify.Infra.Utils;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Crescer.Spotify.Infra.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private IMongoCollection<AlbumOrm> collection;
        private IMusicaRepository musicaRepository;

        public AlbumRepository(MongoAdapter mongoAdapter, IMusicaRepository musicaRepository)
        {
            var database = mongoAdapter.Client.GetDatabase("spotifydotnet");
            collection = database.GetCollection<AlbumOrm>("albums");
            this.musicaRepository = musicaRepository;
        }

        public void AtualizarAlbum(string id, Album album)
        {
            var albumObtido = Obter(id);
            albumObtido?.Atualizar(album);
            if (albumObtido != null)
            {
                var albumOrm = MapearDomainParaOrm(albumObtido);
                var objectId = id.ToObjectId();
                collection.ReplaceOne(x => x.Id.Equals(objectId), albumOrm);
            }
        }

        public void DeletarAlbum(string id)
        {
            var objectId = id.ToObjectId();
            collection.DeleteOne(x => x.Id.Equals(objectId));
        }

        public List<Album> ListarAlbuns()
        {
            List<AlbumOrm> albumOrmList = collection
                .Find<AlbumOrm>(_ => true).ToList();
            var albuns = albumOrmList
                .ConvertAll(new Converter<AlbumOrm, Album>(MapearOrmParaDomain));
            return albuns;
        }

        public Album Obter(string id)
        {
            var objectId = id.ToObjectId();
            var albumOrm = collection
                .Find<AlbumOrm>(x => x.Id.Equals(objectId))
                .FirstOrDefault();

            if (albumOrm == null)
                return null;

            var album = MapearOrmParaDomain(albumOrm);
            return album;
        }

        public void SalvarAlbum(Album album)
        {
            AlbumOrm albumOrm = MapearDomainParaOrm(album);
            collection.InsertOne(albumOrm);
        }

        private Album MapearOrmParaDomain(AlbumOrm albumOrm)
        {
            var listaDeIdDeMusica = albumOrm.ListaDeIdsDeMusica
                .ConvertAll(new Converter<ObjectId, string>(x => x.ToString()));
            var musicas = musicaRepository.ListarMusicas(listaDeIdDeMusica);
            return new Album(albumOrm.Nome, musicas, id: albumOrm.Id.ToString());
        }

        private AlbumOrm MapearDomainParaOrm(Album album)
        {
            var musicaIds = album.Musicas.ConvertAll(new Converter<Musica, ObjectId>(x => x.Id.ToObjectId()));
            return new AlbumOrm(album.Nome, musicaIds, id: album.Id.ToObjectId());
        }
    }
}