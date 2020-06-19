using System.Collections.Generic;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.Dominio.Servicos
{
    public class AlbumService
    {
        private readonly IAlbumRepository albumRepository;
        private readonly MusicaService musicaService;

        public AlbumService(IAlbumRepository albumRepository, MusicaService musicaService)
        {
            this.albumRepository = albumRepository;
            this.musicaService = musicaService;
        }

        public void CriarAlbum(Album album)
        {
            var (_, musicas, nome) = album;

            musicas = musicaService.SalvarMusicasNoRepositorio(musicas);

            var novoAlbum = new Album(nome, musicas);

            albumRepository.SalvarAlbum(novoAlbum);
        }

        public void AtualizarAlbum(string albumId, Album album)
        {
            var (_, musicas, nome) = album;

            musicas = musicaService.SalvarMusicasNoRepositorio(musicas);

            var novoAlbum = new Album(nome, musicas);

            albumRepository.AtualizarAlbum(albumId, novoAlbum);
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