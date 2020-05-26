using System.Collections.Generic;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Dominio.Contratos
{
    public interface IAlbumRepository
    {       
        void SalvarAlbum(Album album);
        void AtualizarAlbum(string id, Album album);
        void DeletarAlbum(string id);
        List<Album> ListarAlbuns();
        Album Obter(string id);
    }
}