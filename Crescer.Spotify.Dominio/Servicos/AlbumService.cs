using System.Collections.Generic;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Dominio.Servicos
{
    public class AlbumService
    {
        public List<string> Validar(Album album)
        {
            List<string> mensagens = new List<string>();

            if (string.IsNullOrEmpty(album.Nome))
                mensagens.Add("É necessário informar o nome do álbum");

            return mensagens;
        }
    }
}