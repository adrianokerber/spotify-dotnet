using System.Collections.Generic;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Dominio.Servicos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crescer.Spotify.Dominio.Tests
{
    [TestClass]
    public class MusicaServiceTests
    {
        [TestMethod]
        public void DeveRetornarErroSeUmNomeNaoForInformado()
        {
            var albumService = new MusicaService();

            var erros = albumService.Validar(new Musica(null, 200));

            CollectionAssert.AreEqual(new List<string> { "É necessário informar o nome da música" }, erros);
        }

        [TestMethod]
        public void DeveRetornarErroSeUmaDuracaoNaoForInformada()
        {
            var albumService = new MusicaService();

            var erros = albumService.Validar(new Musica("Musica 1", 0));

            CollectionAssert.AreEqual(new List<string> { "É necessário informar a duração da música" }, erros);
        }

        [TestMethod]
        public void EmCasoDeMaisDeUmErroTodosDevemSerRetornados()
        {
            var albumService = new MusicaService();

            var erros = albumService.Validar(new Musica(null, 0));

            CollectionAssert.AreEqual(new List<string> { "É necessário informar o nome da música", "É necessário informar a duração da música" }, erros);
        }
    }
}
