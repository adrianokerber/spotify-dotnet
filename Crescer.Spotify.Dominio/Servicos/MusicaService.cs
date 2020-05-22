using System.Collections.Generic;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Dominio.Servicos
{
    public class MusicaService
    {
        public List<string> Validar(Musica musica)
        {
            List<string> mensagens = new List<string>();

            if (string.IsNullOrEmpty(musica.Nome))
                mensagens.Add("É necessário informar o nome da música");

            if (musica.Duracao <= default(double))
                mensagens.Add("É necessário informar a duração da música");

            return mensagens;
        }
    }
}