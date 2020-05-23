using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Infra.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private static List<Album> albuns = new List<Album>();

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