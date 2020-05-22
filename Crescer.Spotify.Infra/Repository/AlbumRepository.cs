using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Infra.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private static List<Album> albuns = new List<Album>();
        private static int idAlbum = 1;

        public void AtualizarAlbum(int id, Album album)
        {
            var albumObtido = Obter(id);
            albumObtido?.Atualizar(album);
        }

        public void DeletarAlbum(int id)
        {
            var album = this.Obter(id);
            albuns.Remove(album);
        }

        public List<Album> ListarAlbum()
        {
            return albuns;
        }

        public Album Obter(int id)
        {
            return albuns.Where(x => x.Id == id).FirstOrDefault();
        }

        public void SalvarAlbum(Album album)
        {
            album.Id = idAlbum++;
            albuns.Add(album);
        }
    }
}