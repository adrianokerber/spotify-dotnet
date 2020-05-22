using System.Collections.Generic;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Dominio.Contratos
{
    public interface IMusicaRepository
    {
        void SalvarMusica(Musica musica);
        void AtualizarMusica(int id, Musica musica);
        void DeletarMusica(int id);
        List<Musica> ListarMusicas();     
        List<Musica> ListarMusicas(List<int> idsMusica);
        Musica Obter(int id);
    }
}