using Kerber.SpotifyLibrary.Domain.Entidades;
using Kerber.SpotifyLibrary.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kerber.SpotifyLibrary.WebApi.Mappers
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
