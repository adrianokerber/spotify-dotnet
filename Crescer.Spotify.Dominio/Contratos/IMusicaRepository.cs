using System.Collections.Generic;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Dominio.Contratos
{
    public interface IMusicaRepository
    {
        void SalvarMusica(Musica musica);
        void AtualizarMusica(string id, Musica musica);
        void DeletarMusica(string id);
        List<Musica> ListarMusicas();
        List<Musica> ListarMusicas(List<string> idsDeMusicas);
        List<Musica> ListarMusicasPorNome(List<string> nomesDeMusicas);
        Musica Obter(string id);
    }
}