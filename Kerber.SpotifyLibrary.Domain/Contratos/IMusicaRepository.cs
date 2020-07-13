using System.Collections.Generic;
using Kerber.SpotifyLibrary.Domain.Entidades;

namespace Kerber.SpotifyLibrary.Domain.Contratos
{
    public interface IMusicaRepository
    {
        Musica SalvarMusica(Musica musica);
        List<Musica> SalvarMusicas(List<Musica> musicas);
        void AtualizarMusica(string id, Musica musica);
        void DeletarMusica(string id);
        List<Musica> ListarMusicas();
        List<Musica> ListarMusicas(List<string> idsDeMusicas);
        List<Musica> ListarMusicasPorNome(List<string> nomesDeMusicas);
        Musica Obter(string id);
    }
}