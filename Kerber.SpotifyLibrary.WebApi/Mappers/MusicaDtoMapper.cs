using Kerber.SpotifyLibrary.Domain.Entidades;
using Kerber.SpotifyLibrary.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kerber.SpotifyLibrary.WebApi.Mappers
{
    public static class MusicaDtoMapper
    {
        public static Musica MapearDtoParaDominio(this MusicaDto musicaDto)
        {
            return new Musica(musicaDto.Nome, musicaDto.Duracao);
        }
    }
}
