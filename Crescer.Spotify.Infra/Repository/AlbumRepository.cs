using System;
using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Infra.Adapters;
using Crescer.Spotify.Infra.Entities;
using MongoDB.Driver;

namespace Crescer.Spotify.Infra.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        [Obsolete("This list will be removed once the methods are migrated to use the DB")]
        private static List<Album> albuns = new List<Album>();
        private IMongoCollection<AlbumOrm> collection;

        public AlbumRepository(MongoAdapter mongoAdapter)
        {
            var database = mongoAdapter.Client.GetDatabase("spotifydotnet");
            collection = database.GetCollection<AlbumOrm>("albums");
        }

        public void AtualizarAlbum(string id, Album album)
        {
            var albumObtido = Obter(id);
            albumObtido?.Atualizar(album);
        }

        public void DeletarAlbum(string id)
        {
            var album = this.Obter(id);
            albuns.Remove(album);
        }

        public List<Album> ListarAlbum()
        {
            return albuns;
        }

        public Album Obter(string id)
        {
            return albuns.Where(x => x.Id.ToString() == id).FirstOrDefault();
        }

        public void SalvarAlbum(Album album)
        {
            albuns.Add(album);
        }
    }
}