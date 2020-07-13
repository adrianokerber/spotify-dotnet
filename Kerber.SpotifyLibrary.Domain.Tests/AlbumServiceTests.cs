using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kerber.SpotifyLibrary.Domain.Servicos;
using Kerber.SpotifyLibrary.Domain.Entidades;
using System.Collections.Generic;
using Kerber.SpotifyLibrary.Domain.Contratos;
using Moq;

namespace Kerber.SpotifyLibrary.Domain.Tests
{
    [TestClass]
    public class AlbumServiceTests
    {
        [TestMethod]
        public void DeveRetornarErroSeUmNomeNaoForInformado()
        {
            Mock<IAlbumRepository> mockRepoAlbumRepository = new Mock<IAlbumRepository>();
            Mock<IMusicaRepository> mockRepoMusicaRepository = new Mock<IMusicaRepository>();
            Mock<MusicaService> mockRepoMusicaService = new Mock<MusicaService>(mockRepoMusicaRepository.Object);
            var albumService = new AlbumService(mockRepoAlbumRepository.Object, mockRepoMusicaService.Object);

            var erros = albumService.Validar(new Album(null));

            CollectionAssert.AreEqual(new List<string> { "É necessário informar o nome do álbum" }, erros);
        }
    }
}
