using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crescer.Spotify.Dominio;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.Dominio.Entidades;
using System.Collections.Generic;

namespace Crescer.Spotify.Dominio.Tests
{
    [TestClass]
    public class AlbumServiceTests
    {
        [TestMethod]
        public void DeveRetornarErroSeUmNomeNaoForInformado()
        {
            var albumService = new AlbumService();

            var erros = albumService.Validar(new Album(null));

            CollectionAssert.AreEqual(new List<string> { "É necessário informar o nome do álbum" }, erros);
        }
    }
}
