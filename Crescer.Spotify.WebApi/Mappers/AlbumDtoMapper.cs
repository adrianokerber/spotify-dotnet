using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crescer.Spotify.WebApi.Mappers
{
    public static class AlbumDtoMapper
    {
        public static Album MapearDtoParaDominio(this AlbumDto albumDto)
        {
            List<Musica> musicas = new List<Musica>();
            var albumTemMusicas = albumDto.Musicas?.Any() ?? false;
            if (albumTemMusicas)
            {
                musicas = albumDto.Musicas
                    .Select(x => x.MapearDtoParaDominio())
                    .ToList();
            }

            return new Album(albumDto.Nome, musicas);
        }
    }
}
