using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Infra.Entities;
using Crescer.Spotify.Infra.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crescer.Spotify.Infra.Mappers
{
    public static class MusicaOrmMapper
    {
        public static Musica MapearOrmParaDomain(MusicaOrm musicaOrm)
        {
            return new Musica(musicaOrm.Nome, musicaOrm.Duracao, id: musicaOrm.Id.ToString());
        }

        public static List<Musica> MapearCollectionOrmParaCollectionDomain(List<MusicaOrm> musicaOrms)
        {
            return musicaOrms.ConvertAll(new Converter<MusicaOrm, Musica>(MapearOrmParaDomain));
        }

        public static MusicaOrm MapearDomainParaOrm(Musica musica)
        {
            return new MusicaOrm(musica.Nome, musica.Duracao, id: musica.Id.ToObjectId());
        }

        public static List<MusicaOrm> MapearCollectionDomainParaCollectionOrm(List<Musica> musicas)
        {
            return musicas.ConvertAll(new Converter<Musica, MusicaOrm>(MapearDomainParaOrm));
        }
    }
}
