using System.Collections.Generic;
using System.Linq;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Dominio.Servicos
{
    public class AlbumService
    {
        private IAlbumRepository albumRepository;
        private IMusicaRepository musicaRepository;

        public AlbumService(IAlbumRepository albumRepository, IMusicaRepository musicaRepository)
        {
            this.albumRepository = albumRepository;
            this.musicaRepository = musicaRepository;
        }

        public void CriarAlbum(Album album)
        {
            var (_, musicas, nome) = album;

            musicas = SalvarMusicasNoRepositorio(musicas);

            var novoAlbum = new Album(nome, musicas);

            albumRepository.SalvarAlbum(novoAlbum);
        }

        public void AtualizarAlbum(string albumId, Album album)
        {
            var (_, musicas, nome) = album;

            musicas = SalvarMusicasNoRepositorio(musicas);

            var novoAlbum = new Album(nome, musicas);

            albumRepository.AtualizarAlbum(albumId, novoAlbum);
        }

        /**
         * Save new songs to the repository and return all (new + existing songs)
         */
        private List<Musica> SalvarMusicasNoRepositorio(List<Musica> musicas)
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

        public List<string> Validar(Album album)
        {
            List<string> mensagens = new List<string>();

            if (string.IsNullOrEmpty(album.Nome))
                mensagens.Add("É necessário informar o nome do álbum");

            return mensagens;
        }
    }
}