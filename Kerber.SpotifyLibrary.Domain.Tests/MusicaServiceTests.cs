using System.Collections.Generic;
using Kerber.SpotifyLibrary.Domain.Contratos;
using Kerber.SpotifyLibrary.Domain.Entidades;
using Kerber.SpotifyLibrary.Domain.Servicos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kerber.SpotifyLibrary.Domain.Tests
{
    [TestClass]
    public class MusicaServiceTests
    {
        private Mock<IMusicaRepository> mockRepoMusicaRepository;

        [TestInitialize]
        public void Before()
        {
            mockRepoMusicaRepository = new Mock<IMusicaRepository>();
        }

        [TestMethod]
        public void DeveRetornarErroSeUmNomeNaoForInformado()
        {
            var musicaService = new MusicaService(mockRepoMusicaRepository.Object);

            var erros = musicaService.Validar(new Musica(null, 200));

            CollectionAssert.AreEqual(new List<string> { "É necessário informar o nome da música" }, erros);
        }

        [TestMethod]
        public void DeveRetornarErroSeUmaDuracaoNaoForInformada()
        {
            var musicaService = new MusicaService(mockRepoMusicaRepository.Object);

            var erros = musicaService.Validar(new Musica("Musica 1", 0));

            CollectionAssert.AreEqual(new List<string> { "É necessário informar a duração da música" }, erros);
        }

        [TestMethod]
        public void EmCasoDeMaisDeUmErroTodosDevemSerRetornados()
        {
            var musicaService = new MusicaService(mockRepoMusicaRepository.Object);

            var erros = musicaService.Validar(new Musica(null, 0));

            CollectionAssert.AreEqual(new List<string> { "É necessário informar o nome da música", "É necessário informar a duração da música" }, erros);
        }
    }
}
