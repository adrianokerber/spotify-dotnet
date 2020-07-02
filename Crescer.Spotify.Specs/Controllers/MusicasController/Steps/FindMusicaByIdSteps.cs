using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Entidades;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Moq;
using TechTalk.SpecFlow;

namespace Crescer.Spotify.Specs.Steps
{
    [Binding]
    public class FindMusicaByIdSteps
    {
        Mock<IMusicaRepository> mockRepoMusicaRepository;
        private MusicasController musicasController;
        private string givenId;
        private string resultId;

        [Before]
        public void Before()
        {
            mockRepoMusicaRepository = new Mock<IMusicaRepository>();
            Mock<MusicaService> mockRepoMusicaService = new Mock<MusicaService>(mockRepoMusicaRepository.Object);

            musicasController = new MusicasController(mockRepoMusicaRepository.Object, mockRepoMusicaService.Object);
        }

        [Given(@"I have the id ""(.*)""")]
        public void GivenIHaveTheId(string id)
        {
            givenId = id;

            var music = new Musica("Music1", 0.0, givenId);
            mockRepoMusicaRepository
                .Setup(repo => repo.Obter(givenId))
                .Returns(music);
        }
        
        [When(@"I call GET music")]
        public void WhenICallGETMusic()
        {
            var objectResult = musicasController.Get(givenId) as ObjectResult;
            var musicFound = objectResult.Value as Musica;

            resultId = musicFound.Id;
        }

        [Then(@"the result should be a music with the same ""(.*)"" id")]
        public void ThenTheResultShouldBeAMusicWithTheSameId(string expectedId)
        {
            Assert.AreEqual(expectedId, resultId);
        }
    }
}
