using System.Collections.Generic;
using Kerber.SpotifyLibrary.Domain.Entidades;

namespace Kerber.SpotifyLibrary.Domain.Contratos
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