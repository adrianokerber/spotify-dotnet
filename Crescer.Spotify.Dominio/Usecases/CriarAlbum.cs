using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Crescer.Spotify.Dominio.Usecases
{
    // TODO: review if usecases should even exist with DDD
    public class CriarAlbum
    {
        private IAlbumRepository albumRepository;
        private IMusicaRepository musicaRepository;
        
        public CriarAlbum(IAlbumRepository albumRepository, IMusicaRepository musicaRepository)
        {
            this.albumRepository = albumRepository;
            this.musicaRepository = musicaRepository;
        }

        public void Invoke(Album album)
        {
            var (_, musicas, nome) = album;

            var nomesDeMusicasParaEncontrar = album.Musicas.Select(x => x.Nome).ToList();
            var musicasEncontradas = musicaRepository.ListarMusicasPorNome(nomesDeMusicasParaEncontrar);

            var temosNovasMusicasParaSalvar = musicasEncontradas.Count < nomesDeMusicasParaEncontrar.Count;

            if (temosNovasMusicasParaSalvar)
            {
                var nomesDeMusicasEncontradas = musicasEncontradas.Select(x => x.Nome);
                var musicasParaSalvar = album.Musicas
                    .Where(x => !nomesDeMusicasEncontradas.Contains(x.Nome))
                    .ToList();

                musicaRepository.SalvarMusicas(musicasParaSalvar);

                musicas = musicasParaSalvar.Union(musicasEncontradas).ToList();
            } else
            {
                musicas = musicasEncontradas;
            }

            var novoAlbum = new Album(nome, musicas);

            albumRepository.SalvarAlbum(novoAlbum);
        }
    }
}
