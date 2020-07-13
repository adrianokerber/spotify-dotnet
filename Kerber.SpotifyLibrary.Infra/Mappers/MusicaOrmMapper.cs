using Kerber.SpotifyLibrary.Domain.Entidades;
using Kerber.SpotifyLibrary.Infra.Entities;
using Kerber.SpotifyLibrary.Infra.Utils;
using System;
using System.Collections.Generic;

namespace Kerber.SpotifyLibrary.Infra.Mappers
{
    public static class MusicaOrmMapper
    {
        public static Musica MapearOrmParaDomain(this MusicaOrm musicaOrm)
        {
            return new Musica(musicaOrm.Nome, musicaOrm.Duracao, id: musicaOrm.Id.ToString());
        }

        public static List<Musica> MapearCollectionOrmParaCollectionDomain(this List<MusicaOrm> musicaOrms)
        {
            return musicaOrms.ConvertAll(new Converter<MusicaOrm, Musica>(MapearOrmParaDomain));
        }

        public static MusicaOrm MapearDomainParaOrm(this Musica musica)
        {
            return new MusicaOrm(musica.Nome, musica.Duracao, id: musica.Id.ToObjectId());
        }

        public static List<MusicaOrm> MapearCollectionDomainParaCollectionOrm(this List<Musica> musicas)
        {
            return musicas.ConvertAll(new Converter<Musica, MusicaOrm>(MapearDomainParaOrm));
        }
    }
}
