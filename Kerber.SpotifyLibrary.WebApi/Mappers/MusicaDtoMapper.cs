using Kerber.SpotifyLibrary.Domain.Entidades;
using Kerber.SpotifyLibrary.WebApi.Models;

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
