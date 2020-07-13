using Kerber.SpotifyLibrary.Domain.Contratos;
using Kerber.SpotifyLibrary.Domain.Entidades;
using Kerber.SpotifyLibrary.Infra.Adapters;
using Kerber.SpotifyLibrary.Infra.Entities;
using Kerber.SpotifyLibrary.Infra.Mappers;
using Kerber.SpotifyLibrary.Infra.Utils;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Kerber.SpotifyLibrary.Infra.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private IMongoCollection<AlbumOrm> collection;
        private IMusicaRepository musicaRepository;

        public AlbumRepository(MongoAdapter mongoAdapter, IMusicaRepository musicaRepository)
        {
            collection = mongoAdapter.GetCollection<AlbumOrm>("albums");
            this.musicaRepository = musicaRepository;
        }

        public void AtualizarAlbum(string id, Album album)
        {
            var albumObtido = Obter(id);
            if (albumObtido != null)
            {
                albumObtido?.Atualizar(album);

                var albumOrm = albumObtido.MapearDomainParaOrm();
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
            return albumOrmList.MapearCollectionOrmParaCollectionDomain(musicaRepository);
        }

        public Album Obter(string id)
        {
            var objectId = id.ToObjectId();
            var albumOrm = collection
                .Find<AlbumOrm>(x => x.Id.Equals(objectId))
                .FirstOrDefault();

            if (albumOrm == null)
                return null;

            return albumOrm.MapearOrmParaDomain(musicaRepository);
        }

        public void SalvarAlbum(Album album)
        {
            AlbumOrm albumOrm = album.MapearDomainParaOrm();
            collection.InsertOne(albumOrm);
        }
    }
}