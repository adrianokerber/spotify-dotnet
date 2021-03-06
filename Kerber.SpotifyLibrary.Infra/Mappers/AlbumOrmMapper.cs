﻿using Kerber.SpotifyLibrary.Domain.Contratos;
using Kerber.SpotifyLibrary.Domain.Entidades;
using Kerber.SpotifyLibrary.Infra.Entities;
using Kerber.SpotifyLibrary.Infra.Utils;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Kerber.SpotifyLibrary.Infra.Mappers
{
    public static class AlbumOrmMapper
    {
        public static Album MapearOrmParaDomain(this AlbumOrm albumOrm, IMusicaRepository musicaRepository)
        {
            var listaDeIdDeMusica = albumOrm.ListaDeIdsDeMusica
                .ConvertAll(new Converter<ObjectId, string>(x => x.ToString()));
            var musicas = musicaRepository.ListarMusicas(listaDeIdDeMusica);
            return new Album(albumOrm.Nome, musicas, id: albumOrm.Id.ToString());
        }

        public static List<Album> MapearCollectionOrmParaCollectionDomain(this List<AlbumOrm> listaDeAlbumOrm, IMusicaRepository musicaRepository)
        {
            return listaDeAlbumOrm.ConvertAll(new Converter<AlbumOrm, Album>(x => MapearOrmParaDomain(x, musicaRepository)));
        }

        public static AlbumOrm MapearDomainParaOrm(this Album album)
        {
            var musicaIds = album.Musicas.ConvertAll(new Converter<Musica, ObjectId>(x => x.Id.ToObjectId()));
            return new AlbumOrm(album.Nome, musicaIds, id: album.Id.ToObjectId());
        }
    }
}
