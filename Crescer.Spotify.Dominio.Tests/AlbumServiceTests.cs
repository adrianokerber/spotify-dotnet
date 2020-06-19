using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crescer.Spotify.Dominio;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.Dominio.Entidades;
using System.Collections.Generic;
using Crescer.Spotify.Dominio.Contratos;
using Moq;

namespace Crescer.Spotify.Dominio.Tests
{
    [TestClass]
    public class AlbumServiceTests
    {
        [TestMethod]
        public void DeveRetornarErroSeUmNomeNaoForInformado()
        {
            Mock<IAlbumRepository> mockRepoAlbumRepository = new Mock<IAlbumRepository>();
            Mock<IMusicaRepository> mockRepoMusicaRepository = new Mock<IMusicaRepository>();
            var albumService = new AlbumService(mockRepoAlbumRepository.Object, mockRepoMusicaRepository.Object);

            var erros = albumService.Validar(new Album(null));

            CollectionAssert.AreEqual(new List<string> { "É necessário informar o nome do álbum" }, erros);
        }
    }
}
