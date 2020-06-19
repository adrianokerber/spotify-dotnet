using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Dominio.Servicos
{
    public class MusicaService
    {
        private readonly IMusicaRepository musicaRepository;

        public MusicaService(IMusicaRepository musicaRepository)
        {
            this.musicaRepository = musicaRepository;
        }

        /**
         * Save new songs to the repository and return all (new + existing songs)
         */
        public List<Musica> SalvarMusicasNoRepositorio(List<Musica> musicas)
        {
            var musicasEncontradas = BuscarMusicasPorNome(musicas);

            var temosNovasMusicasParaSalvar = musicasEncontradas.Count < musicas.Count;

            if (temosNovasMusicasParaSalvar)
            {
                var musicasParaSalvar = ObterMusicasParaSalvar(musicas, musicasEncontradas);

                var musicasCriadas = musicaRepository.SalvarMusicas(musicasParaSalvar);

                musicas = musicasCriadas.Union(musicasEncontradas).ToList();
            }
            else
            {
                musicas = musicasEncontradas;
            }

            return musicas;
        }

        private List<Musica> BuscarMusicasPorNome(List<Musica> musicas)
        {
            var nomesDeMusicasParaEncontrar = musicas.Select(x => x.Nome).ToList();
            return musicaRepository.ListarMusicasPorNome(nomesDeMusicasParaEncontrar);
        }

        private List<Musica> ObterMusicasParaSalvar(List<Musica> musicas, List<Musica> musicasParaIgnorar)
        {
            var nomesDeMusicasParaIgnorar = musicasParaIgnorar.Select(x => x.Nome);
            return musicas.Where(x => !nomesDeMusicasParaIgnorar.Contains(x.Nome)).ToList();
        }

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