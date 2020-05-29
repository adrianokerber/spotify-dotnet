using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crescer.Spotify.WebApi.Mappers
{
    public static class MusicaDtoMapper
    {
        public static Musica MapearDtoParaDominio(this MusicaDto musicaDto)
        {
            return new Musica(musicaDto.Nome, musicaDto.Duracao);
        }
    }
}
